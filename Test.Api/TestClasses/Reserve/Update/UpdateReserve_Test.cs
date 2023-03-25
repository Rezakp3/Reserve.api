﻿using FluentResults;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Reserve.Update
{
    public class UpdateReserve_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public UpdateReserve_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Update_ValidObjectPass_Return200()
        {
            var reserve = new
            {
                Id = Guid.Parse(""),
                ReserveDate = DateTime.Now.AddDays(6),
                LocationId = Guid.Parse(""),
                Price = 70000
            };


            var myContent = JsonConvert.SerializeObject(reserve);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PutAsync("/api/Reserve/Update", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
