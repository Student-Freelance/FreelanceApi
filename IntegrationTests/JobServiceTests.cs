using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api;
using Freelance_Api.DatabaseContext;
using Freelance_Api.Helpers;
using Freelance_Api.Models;
using Freelance_Api.Models.Requests;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Driver;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class JobServiceTests
    {
        
         [Fact]
         public void Write_Read_Database_Test()
         {
             // Arrange
                    var dbcontext = new MongoDbContext("mongodb://localhost",
                        "Test",
                        "Jobs");
                    IMongoDbContext context;
                    context = dbcontext;
                    var service = new JobService(context);

                    // Act
                    JobModel model = new JobModel{Experience = "Junior", Title = "New"};
                    var result = service.Create(model);
                    var respJob = service.Get(result);
                    // Assert
                    Assert.Equal(model.Title,respJob.Title);

         }
                
      
    }
}