using System;
using System.Collections.Generic;
using ContactsWebApi.Models;

namespace ContactsWebApi.Services
{
    public interface IContactService
    {
        List<Contact> FindContacts();
        Contact FindContactById(long id);
        int AddContact(Contact contanct);
        Tuple<int, Contact> DeleteContact(long id);
        Tuple<int, Contact> UpdateContact(long id, Contact contact);
    }
}
