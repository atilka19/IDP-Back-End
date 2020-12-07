﻿using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Interface
{
    public interface ICommentRepository
    {
        // CREATE
        void CreateCommentAddToTask(int taskID, string text, string userName);
        // DELETE
        void DeleteComment(int ID);

        // UPDATE
        void UpdateCommentText(int ID, string newText);
    }
}
