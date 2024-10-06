using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using System.Collections.Generic;
using System.Linq;


public class GameController : Controller
{
    private static List<Animal> animals = new List<Animal>
    {
        new Animal { Id = 1, Name = "köpek", ImageUrl = "/images/animals/köpek.jpg" },
        new Animal { Id = 2, Name = "tavuk", ImageUrl = "/images/animals/tavuk.jpg" },
        new Animal { Id = 3, Name = "deve", ImageUrl = "/images/animals/deve.jpg" },
        new Animal { Id = 4, Name = "inek", ImageUrl = "/images/animals/inek.jpg" },
        new Animal { Id = 5, Name = "koç", ImageUrl = "/images/animals/koç.jpg" },
        new Animal { Id = 6, Name = "öküz", ImageUrl = "/images/animals/öküz.jpg" },
        new Animal { Id = 7, Name = "koyun", ImageUrl = "/images/animals/koyun.jpg" },
        new Animal { Id = 8, Name = "kartal", ImageUrl = "/images/animals/kartal.jpg" },
        new Animal { Id = 9, Name = "at", ImageUrl = "/images/animals/at.jpg" },
        new Animal { Id = 10, Name = "horoz", ImageUrl = "/images/animals/horoz.jpg" },
        new Animal { Id = 11, Name = "kedi", ImageUrl = "/images/animals/kedi.jpg" }
    };

    private static Random random = new Random();
    private static string currentAnimalName;
    private static int wrongAttempts = 0;

    public IActionResult Index()
    {
        // İlk hayvan ismini rastgele seç
        currentAnimalName = GetRandomAnimalName();
        ViewBag.CurrentAnimalName = currentAnimalName;
        ViewBag.WrongAttempts = wrongAttempts;
        return View(animals);
    }

    [HttpPost]
    public IActionResult CheckAnswer(int id)
    {
        var selectedAnimal = animals.FirstOrDefault(a => a.Id == id);

        if (selectedAnimal != null && selectedAnimal.Name == currentAnimalName)
        {
            // Eğer tıklanan hayvan ismi doğruysa, resmi listeden çıkar ve yeni bir isim getir
            animals.Remove(selectedAnimal);
            currentAnimalName = GetRandomAnimalName();
            ViewBag.CurrentAnimalName = currentAnimalName;
        }
        else
        {
            // Eğer yanlış tıklanmışsa hata puanı artır
            wrongAttempts++;
        }

        ViewBag.WrongAttempts = wrongAttempts;
        return PartialView("_GamePartial", animals); // PartialView ile güncelle
    }

    private string GetRandomAnimalName()
    {
        if (animals.Count == 0)
            return null; // Eğer hayvan kalmadıysa null döndür

        int index = random.Next(animals.Count);
        return animals[index].Name; // Rastgele bir hayvan ismi döndür
    }
}

