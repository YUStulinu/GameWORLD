using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GameWORLD.Controllers
{
    [Authorize(Roles = "Administrator,User")]
    public class PagesController: Controller
    {
        public IActionResult Altele()
        {
            return View();
        }
        public IActionResult Conectare_Inregistrare()
        {
            return View();
        }

        public IActionResult Contacte()
        {
            return View();

        }

        public IActionResult Cumparaturi()
        {
            return View();

        }

        public IActionResult DespreNoi()
        {
            return View();
        }

       
        public IActionResult HTMLPageGame()
        {
            return View();
        }

        public IActionResult JocuriVideo()
        {
            return View();
        }

        public IActionResult JocuriPC()
        {
            return View();
        }

        public IActionResult JocuriPlaystation()
        {
            return View();
        }

        public IActionResult JocuriXbox()
        {
            return View();
        }

        public IActionResult JocuriNintendo()
        {
            return View();
        }

        public IActionResult JocurideTop()
        {
            return View();
        }

        public IActionResult CelemaiVandute()
        {
            return View();
        }

        public IActionResult CautaredupaGenulJocului()
        {
            return View();
        }

        public IActionResult FinalizareComanda()
        {
            return View();
        }

    }
}
