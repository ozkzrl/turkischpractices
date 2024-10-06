using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication11.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class KelimeController : ControllerBase
    {
        private static readonly char[] SesliHarflerKalın = { 'a', 'ı', 'o', 'u' }; // Kalın ünlüler (Büyük Ünlü Uyumu)
        private static readonly char[] SesliHarflerİnce = { 'e', 'i', 'ö', 'ü' }; // İnce ünlüler (Büyük Ünlü Uyumu)
        private static readonly char[] YuvarlakSesli = { 'o', 'ö', 'u', 'ü' }; // Küçük Ünlü Uyumu: Yuvarlak ünlüler
        private static readonly char[] DuzGenisSesli = { 'a', 'e' }; // Küçük Ünlü Uyumu: Düz-geniş ünlüler
        private static readonly char[] YuvarlakDarSesli = { 'u', 'ü' }; // Küçük Ünlü Uyumu: Yuvarlak-dar ünlüler
        private static readonly char[] SessizHarfler = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 's', 't', 'v', 'y', 'z' };

        private static string sonSesliHarf = null;
        private static bool kontrolKucukUnluUyumu = false;

        // Rastgele sessiz harf döndüren endpoint
        [HttpGet("sessiz-harf")]
        public ActionResult<char> GetSessizHarf()
        {
            var random = new Random();
            return Ok(SessizHarfler[random.Next(SessizHarfler.Length)]);
        }

        // Sesli harf ve ünlü uyumu kontrolü
        [HttpPost("kontrol")]
        public ActionResult<string> KontrolSesliHarf([FromBody] string yeniSesliHarf)
        {
            if (string.IsNullOrEmpty(sonSesliHarf))
            {
                sonSesliHarf = yeniSesliHarf; // İlk harfi kaydet
                return Ok("İlk sesli harf eklendi.");
            }

            bool sonSesliKalın = Array.Exists(SesliHarflerKalın, harf => harf.ToString() == sonSesliHarf);
            bool yeniSesliKalın = Array.Exists(SesliHarflerKalın, harf => harf == yeniSesliHarf[0]);

            // Büyük ünlü uyumu kontrolü
            if (sonSesliKalın && yeniSesliKalın || !sonSesliKalın && !yeniSesliKalın)
            {
                // Büyük ünlü uyumuna uygun, küçük ünlü uyumu kontrolüne geç
                kontrolKucukUnluUyumu = Array.Exists(YuvarlakSesli, harf => harf.ToString() == sonSesliHarf);
                sonSesliHarf = yeniSesliHarf;

                // Eğer küçük ünlü uyumu gerekli değilse başarılı mesajı dön
                if (!kontrolKucukUnluUyumu)
                {
                    return Ok("Büyük ünlü uyumuna uygun.");
                }
            }
            else
            {
                return BadRequest("Büyük ünlü uyumuna aykırı! Lütfen başka bir sesli harf seçin.");
            }

            // Küçük ünlü uyumu kontrolü
            bool yeniSesliYuvarlakDar = Array.Exists(YuvarlakDarSesli, harf => harf == yeniSesliHarf[0]);
            bool yeniSesliDuzGenis = Array.Exists(DuzGenisSesli, harf => harf == yeniSesliHarf[0]);

            if (kontrolKucukUnluUyumu && (yeniSesliYuvarlakDar || yeniSesliDuzGenis))
            {
                return Ok("Küçük ünlü uyumuna uygun.");
            }
            else
            {
                return BadRequest("Küçük ünlü uyumuna aykırı! Lütfen başka bir sesli harf seçin.");
            }
        }
    }
}


