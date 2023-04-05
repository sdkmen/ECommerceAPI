using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.Services;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //test controller
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            //wwwroot/resource/product-images
            //string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/product-images");

            //if(!Directory.Exists(uploadPath))
            //    Directory.CreateDirectory(uploadPath);

            //foreach (IFormFile file in Request.Form.Files)
            //{
            //    DateTime date = DateTime.Now;
            //    string fileName = Path.GetFileNameWithoutExtension(file.FileName);

            //    string fileNameWithDate = Path.Combine(fileName, date.ToString());

            //    string noExtensionFileName = fileNameWithDate.ToLower()
            //        .Replace(" ", "-").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o")
            //        .Replace("ü", "u").Replace("ş", "s").Replace("ç", "c").Replace("Ç", "c")
            //        .Replace("Ş", "s").Replace("Ğ", "g").Replace("Ü", "u").Replace("İ", "i")
            //        .Replace("Ö", "o").Replace("\\","-").Replace(".","-").Replace(":","-").Trim();

            //    string fullPath = Path.Combine(uploadPath, $"{noExtensionFileName}{Path.GetExtension(file.FileName)}");

            //    using FileStream fileStream = new(fullPath,FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            //    await file.CopyToAsync(fileStream);
            //    await fileStream.FlushAsync();
            //}

            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);

            return Ok();
        }
    }
}
