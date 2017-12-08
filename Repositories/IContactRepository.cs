using System;
using System.Collections.Generic;
using ContactsWebApi.Models;

namespace ContactsWebApi.Repositories
{
    public interface IContactRepository
    {
        List<Contact> GetAll();
        Contact GetById(long id);
        int Add(Contact contact);
        Tuple<int, Contact> Delete(long id);
        Tuple<int, Contact> Update(long id, Contact contact);
    }
}
