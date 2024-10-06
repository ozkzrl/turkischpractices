using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class FiillerController : Controller
    {
        private readonly List<Fiil> _fiiller = new List<Fiil>
    {
        new Fiil { Id = 1, FiilAdi = "Yemek", ResimUrl = "/images/yemek.jpg" },
        new Fiil { Id = 2, FiilAdi = "okumak", ResimUrl = "/images/okumak.jpg" },
        new Fiil { Id = 3, FiilAdi = "Yazmak", ResimUrl = "/images/yazmak.jpg" },
        new Fiil { Id = 4, FiilAdi = "Voleybol oynamak", ResimUrl = "/images/voleybol.jpg" },
        new Fiil { Id = 5, FiilAdi = "Gitar çalmak", ResimUrl = "/images/Gitar.jpg" },
        new Fiil { Id = 6, FiilAdi = "Basketbol oynamak", ResimUrl = "/images/Basketbol.jpg" },
        new Fiil { Id = 7, FiilAdi = "Futbol oynamak", ResimUrl = "/images/Futbol.jpg" },
        new Fiil { Id = 8, FiilAdi = "Resim yapmak", ResimUrl = "/images/Resim.jpg" },
        new Fiil { Id = 9, FiilAdi = "Koşmak", ResimUrl = "/images/kosmak.jpg" },
        new Fiil { Id = 10, FiilAdi = "Yüzmek", ResimUrl = "/images/yuzmek.jpg" }
    };

        public IActionResult Index()
        {
            return View(_fiiller);
        }

        [HttpPost]
        public IActionResult EslesmeKontrol(List<int> secilenFiiller, List<int> secilenResimler)
        {
            bool dogruMu = secilenFiiller.SequenceEqual(secilenResimler);
            return Json(new { success = dogruMu });
        }
    }
}
