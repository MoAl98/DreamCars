using Cars.Data;
using Cars.Models;
using Cars.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Controllers
{
    public class CarController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public CarController(MVCDbContext mvcDbContext) 
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          var cars = await mvcDbContext.Cars.ToListAsync();   
            return View(cars);  
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]  
        public async Task<IActionResult> Add(AddCarViewModel addCarRequest)//add data 
        {
            var car = new Car()//create new object
            {
                Id = Guid.NewGuid(),
                Modell = addCarRequest.Modell,
                Brand = addCarRequest.Brand,
                Price = addCarRequest.Price,
                ManufactureDate = addCarRequest.ManufactureDate,
                category = addCarRequest.category

            };
            await mvcDbContext.Cars.AddAsync(car);//add to the database
            await mvcDbContext.SaveChangesAsync();//save the new data
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)// get data from database to show it 
        {
           var car =  await mvcDbContext.Cars.FirstOrDefaultAsync(x=> x.Id == id);    

            if (car != null)
            {
                var viewModel = new UpdateCarViewModel()
                {
                    Id = car.Id,
                    Modell = car.Modell,
                    Brand = car.Brand,
                    Price = car.Price,
                    ManufactureDate = car.ManufactureDate,
                    category = car.category

                };
                return await Task.Run(() => View("View", viewModel));
            }
           
            return RedirectToAction("Index");
        }
        [HttpPost]  
        public async Task<IActionResult> View(UpdateCarViewModel model)//get the new information 
        {
            var car = await mvcDbContext.Cars.FindAsync(model.Id);
            if(car != null) 
            { 
                car.Modell=model.Modell;
                car.Brand = model.Brand;
                car.Price = model.Price ;
                car.ManufactureDate = model.ManufactureDate ;
                car.category = model.category;

               await mvcDbContext.SaveChangesAsync();//update the data in the database
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCarViewModel model)
        {
            var car = await mvcDbContext.Cars.FindAsync(model.Id);
            if (car != null)
            {
                mvcDbContext.Cars.Remove(car);   
                
                await mvcDbContext.SaveChangesAsync();  // remove data from the database
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
