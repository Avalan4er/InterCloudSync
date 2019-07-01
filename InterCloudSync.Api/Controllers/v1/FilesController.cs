using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterCloudSync.Business;
using InterCloudSync.Cloud;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterCloudSync.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class FilesController : Controller
    {
        private readonly DriverManager _driverManager = new DriverManager();

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<IEntry>> Get(CancellationToken ct)
        {
            var selectedDriver = _driverManager.GoogleDrive;

            if (!selectedDriver.IsAuthenticated)
            {
                await selectedDriver.Authenticate("avalan4er@gmail.com", ct).ConfigureAwait(false);
            }

            return await selectedDriver.GetFiles(ct).ConfigureAwait(false);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}