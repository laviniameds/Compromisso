using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ComproController : ApiController
    {
        // GET api/fabricante
        public IEnumerable<Models.Compromisso> Get()
        {
            Models.ComproDataContext dc = new Models.ComproDataContext();
            var r = from f in dc.Compromissos select f;
            return r.ToList();
        }

        // POST api/fabricante
        public void Post([FromBody] string value)
        {
            List<Models.Compromisso> x = JsonConvert.DeserializeObject<List<Models.Compromisso>>(value);
            Models.ComproDataContext dc = new Models.ComproDataContext();
            dc.Compromissos.InsertAllOnSubmit(x);
            dc.SubmitChanges();
        }

        // PUT api/fabricante/5
        public void Put(int id, [FromBody] string value)
        {
            Models.Compromisso x = JsonConvert.DeserializeObject<Models.Compromisso>(value);
            Models.ComproDataContext dc = new Models.ComproDataContext();
            Models.Compromisso fab = (from f in dc.Compromissos
                                     where f.id == id
                                     select f).Single();
            fab.descricao = x.descricao;
            fab.local = x.local;
            fab.data = x.data;
            fab.realizado = x.realizado;
            dc.SubmitChanges();
        }

        // DELETE api/fabricante/5
        public void Delete(int id)
        {
            Models.ComproDataContext dc = new Models.ComproDataContext();
            Models.Compromisso fab = (from f in dc.Compromissos
                                      where f.id == id
                                     select f).Single();
            dc.Compromissos.DeleteOnSubmit(fab);
            dc.SubmitChanges();
        }
    }
}
