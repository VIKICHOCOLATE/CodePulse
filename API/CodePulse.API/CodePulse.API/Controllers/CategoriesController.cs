﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	// https://localhost:xxxx/api/categories
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
		{
			//Map DTO to Domain Model
			var category = new Category
			{
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			await categoryRepository.CreateAsync(category);

			var response = new CategoryDto
			{
				Id = category.Id,
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			return Ok(response);
		}


		// GET: /api/categories
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await categoryRepository.GetAllAsync();

			// Map Domain model to DTO

			var response = new List<CategoryDto>();
			foreach (var category in categories)
			{
				response.Add(new CategoryDto
				{
					Id = category.Id,
					Name = category.Name,
					UrlHandle = category.UrlHandle
				});
			}

			return Ok(response);
		}
	}
}
