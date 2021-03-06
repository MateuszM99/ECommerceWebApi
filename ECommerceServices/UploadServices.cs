﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServices
{
    public class UploadServices : IUploadServices
    {
        private readonly ECommerceContext appDb;
        public IConfiguration Configuration;
     
        public UploadServices(ECommerceContext appDb, IConfiguration configuration)
        {
            this.appDb = appDb;         
            Configuration = configuration;
        }       

        public async Task UploadProductPhotoAsync(IFormFile imageFile,Product product)
        {         
            Account cloudinaryAccount = new Account(Configuration["Cloudinary:CloudName"], Configuration["Cloudinary:CloudApiKey"], Configuration["Cloudinary:CloudApiKeySecret"]);
        
            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
                   
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(product.Name,imageFile.OpenReadStream()),
                PublicId = String.Format("EcommerceWebsite/productPhotos/{0}",product.Id),
                Overwrite = true                
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            string url = uploadResult.Url.ToString();

            product.ImageUrl = url;

            await appDb.SaveChangesAsync();
        }
    }
}
