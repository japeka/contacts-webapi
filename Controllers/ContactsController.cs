using System;
using Microsoft.AspNetCore.Mvc;
using ContactsWebApi.Models;
using ContactsWebApi.Services;

namespace ContactsWebApi
{
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        // GET api/contacts
        [HttpGet]
        public IActionResult Get()
        {
            var contacts = _contactService.FindContacts();
            return new JsonResult(contacts);
        }

        // GET api/contacts/:id
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var contact = _contactService.FindContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return new JsonResult(contact);
        }

        // POST api/contacts
        [HttpPost]
        public IActionResult Post([FromBody]Contact contact)
        {
            int retCode = _contactService.AddContact(contact);
            if (retCode == 1) {
                return Ok(contact);
            } else if (retCode == 0) {
                return BadRequest();
            } else  { // - 1 or other cases return 404
                return StatusCode(404);
            }
        }

        // PUT api/contacts/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Contact contact)
        {
            Tuple<int,Contact> ret = _contactService.UpdateContact(id, contact);
            if (ret.Item1 == 1) {
               return Ok(ret.Item2);
            } else if (ret.Item1 == 0) {
               return BadRequest();
            } else if (ret.Item1 == -1) {
               return NotFound();
            } else {
               return StatusCode(404);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Tuple<int,Contact> ret = _contactService.DeleteContact(id);
            if (ret.Item1 == 1) { //item removed
              return Ok(ret.Item2); 
            }  else if (ret.Item1 == 0) { //item not found
              return NotFound();
            } else { //item was not removed
              return StatusCode(404);
            }
        }

    }
}
