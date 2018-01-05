using System;
using System.Collections.Generic;
using ContactsWebApi.Models;
using ContactsWebApi.Repositories;
using ContactsWebApi.Shared;

namespace ContactsWebApi.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public List<Contact> FindContacts()
        {
            var contacts = _contactRepository.GetAll();
            return contacts;
        }

        public Contact FindContactById(long id)
        {
            return _contactRepository.GetById(id);
        }

        public int AddContact(Contact contact)
        {
            if (contact.Avatar == null) {
               var avatar = new AvatarGenerator();
               contact.Avatar = avatar.GetAvatarPicture(contact.Gender);
            }
            return _contactRepository.Add(contact);
        }

        public Tuple<int,Contact> UpdateContact(long id, Contact contact) {
            if (contact == null || contact.Id != id) {
                return new Tuple<int, Contact>(0, null); 
            }
            return _contactRepository.Update(id, contact);
        }

        public Tuple<int,Contact> DeleteContact(long id)  {

            return _contactRepository.Delete(id);
        }



    }
}
