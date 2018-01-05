using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ContactsWebApi.Config;
using ContactsWebApi.Models;
using ContactsWebApi.Shared;

namespace ContactsWebApi.Repositories
{
    public class ContactRepository : IContactRepository
    {

        private readonly ContactContext _context;

        public ContactRepository(ContactContext context)
        {
            _context = context;
            /*if (!_context.Contacts.Any()) {
                Initialize();
            }*/
        }

        //Get All Contacts
        public List<Contact> GetAll() {
            return _context.Contacts.
                OrderBy(c => c.LastName).
                    ThenBy(c => c.FirstName).
                        ToList();
        }

        //Get One Contact By Id
        public Contact GetById(long id) {
            return _context.Contacts.FirstOrDefault(t => t.Id == id);
        }

        //Add One New Contact
        public int Add(Contact contact) {
            if (contact == null)
                return 0;
            _context.Contacts.Add(contact);
            return _context.SaveChanges() > 0 ? 1 : -1;
        }

        //Update An Existing Contact        
        public Tuple<int, Contact> Update(long id, Contact contact) {

            //Find the contact
            var _contact = _context.Contacts.FirstOrDefault(t => t.Id == id);
            if (_contact == null) {
              return new Tuple<int, Contact>(-1, null); //Not Found  -1, null
            }

            //Update all fields cause we don't know which fields have been updated
            _contact.FirstName = contact.FirstName;
            _contact.LastName = contact.LastName;
            _contact.Phone = contact.Phone;
            _contact.City = contact.City;
            _contact.StreetAddress = contact.StreetAddress;
            _contact.Avatar = contact.Gender == _contact.Gender ? contact.Avatar : new AvatarGenerator().GetAvatarPicture(contact.Gender); 
            _contact.Gender = contact.Gender;
            _context.Contacts.Update(_contact);

            //return success: 1, contact OR failure: -2, null
            return _context.SaveChanges() > 0  
                ? new Tuple<int, Contact>(1, _contact) 
                    : new Tuple<int, Contact>(-2, null);

        }

        //Delete an existing contact
        public Tuple<int,Contact> Delete( long id ) {
            var contact = _context.Contacts.FirstOrDefault(t => t.Id == id);
            if (contact == null) {
                return new Tuple<int, Contact>(0, null); //0 not found
            }

            _context.Contacts.Remove(contact);
            int ret = _context.SaveChanges();
            return ret > 0
                ? new Tuple<int, Contact>(1, contact)
                    : new Tuple<int, Contact>(-1, null); 
        }

        //Populate contacts with 3 new contacts in case db is empty
        private void Initialize() {
            _context.Contacts.Add(new Contact { FirstName = "aarne", LastName = "samuelsson", Phone = "02-929999991", Gender = 1, StreetAddress = "rastaankatu 3", City = "Imatra", Avatar = "http://svgavatars.com/style/svg/03.svg" });
            _context.SaveChanges();
            _context.Contacts.Add(new Contact { FirstName = "Paula", LastName = "Roslöf", Phone = "040-20440022", Gender = 0, StreetAddress = "purjehtijankatu 34", City = "Rauma", Avatar = "http://svgavatars.com/style/svg/18.svg" });
            _context.SaveChanges();
            _context.Contacts.Add(new Contact { FirstName = "Kalle", LastName = "Mäntylä", Phone = "09-292929012", Gender = 1, StreetAddress = "ilkankatu 1", City = "lappeenranta", Avatar = "http://svgavatars.com/style/svg/15.svg" });
            _context.SaveChanges();
        }

    }
}
