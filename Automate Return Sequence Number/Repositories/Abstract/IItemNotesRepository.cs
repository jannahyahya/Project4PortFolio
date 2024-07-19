using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface IItemNotesRepository
    {
        IEnumerable<ItemNotes> GetItemNotes(int ServerId, bool isActive = false);
        IEnumerable<ItemNotes> GetItemNotesBySearch(int ServerId, string POSO = null);
        ItemNotes IsExistItemNotes(int ServerId, string PO = null, string SO =null);
        ItemNotes GetItemNotesByPOSO(int ServerId, string SO, string SOLINE = null);
        int UpdateItemNotesBySO(int ServerId, ItemNotes objItemNotes);
        int InsertItemNotes(int ServerId, ItemNotes objItemNotes);
    }
}