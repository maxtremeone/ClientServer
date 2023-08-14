
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Handlers;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Client.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository repository;

        public RoomController(IRoomRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var ListRoom = new List<Room>();

            if (result.Data != null)
            {
                ListRoom = result.Data.ToList();
            }
            return View(ListRoom);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewRoomDto newRoom)
        {

            var result = await repository.Post(newRoom);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await repository.Get(id);
            var ListRoom = new Room();

            if (result.Data != null)
            {
                ListRoom = result.Data;
            }
            return View(ListRoom);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Room room)
        {
            var result = await repository.Put(room.Guid, room);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Room");
            }
            return RedirectToAction(nameof(Edit));
        }
    }
}