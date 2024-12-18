using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {

        private static List<Musicians> _musicians = new List<Musicians>
        {
            new Musicians { Id = 1, Name = "Ahmet Çalgı", Job = "Ünlü Çalgı Çalar", FunnyFeatures = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new Musicians { Id = 2, Name = "Zeynep Melodi", Job = "Popüler Melodi Yazarı", FunnyFeatures = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new Musicians { Id = 3, Name = "Cemil Akor", Job = "Çılgın Akorist", FunnyFeatures = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            new Musicians { Id = 4, Name = "Fatma Nota", Job = "Sürpriz Nota Üreticisi", FunnyFeatures = "Nota üretirken sürekli sürprizler hazırlar" },
            new Musicians { Id = 5, Name = "Hasan Ritim", Job = "Ritim Canavarı", FunnyFeatures = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new Musicians { Id = 6, Name = "Elif Armoni", Job = "Armoni Ustası", FunnyFeatures = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
            new Musicians { Id = 7, Name = "Ali Perde", Job = "Perde Uygulayıcı", FunnyFeatures = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new Musicians { Id = 8, Name = "Ayşe Rezonans", Job = "Rezonans Uzmanı", FunnyFeatures = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
            new Musicians { Id = 9, Name = "Murat Ton", Job = "Tonlama Meraklısı", FunnyFeatures = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç" },
            new Musicians { Id = 10, Name = "Selin Akor", Job = "Akor Sihirbazı", FunnyFeatures = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
        };

        [HttpGet]
        public IEnumerable<Musicians> GetAll()
        {
            return _musicians;
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<Musicians> GetMusician(int id)
        {
            var tour = _musicians.FirstOrDefault(x => x.Id == id);
            if (tour != null)
            {
                return NotFound($" Id {id} is  not found.");
            }
            return Ok(tour);
        }

        [HttpPost]
        public ActionResult<Musicians> Create([FromBody] Musicians musician)
        {
            var id = _musicians.Max(t => t.Id) + 1;
            musician.Id = id;

            _musicians.Add(musician);

            return CreatedAtAction(nameof(GetMusician), new { id = musician.Id }, musician);
        }

        [HttpPatch("{id}")]
        public ActionResult PatchMusician(int id, [FromQuery] string job)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician == null)
            {
                return NotFound();
            }

            musician.Job = job;

            return Ok(musician);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTourPlanet(int id, UpdateMusicians updatedMusician)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician == null)
            {
                return NotFound($"Muzisyen id {id} bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(updatedMusician.Name) ||
                string.IsNullOrWhiteSpace(updatedMusician.Job) ||
                string.IsNullOrWhiteSpace(updatedMusician.FunnyFeatures))
            {
                return BadRequest("Name, Job, ve FunnyFeatures boş olamaz");
            }

            musician.Name = updatedMusician.Name;
            musician.Job = updatedMusician.Job;
            musician.FunnyFeatures = updatedMusician.FunnyFeatures;


            return Ok(musician);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician == null)
            {
                return NotFound($"Muzisyen id {id} bulunamadı.");
            }

            _musicians.Remove(musician);

            return NoContent();
        }
    }

}
