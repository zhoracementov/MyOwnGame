﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public string Name { get; set; }
        public ObservableCollection<QuestionsLine> TableLines { get; set; }

        public QuestionsTable()
        {
            //...
        }

        public QuestionsTable(string name, IEnumerable<QuestionsLine> table)
        {
            Name = name;
            TableLines = new ObservableCollection<QuestionsLine>(table);
        }

        public QuestionsTable(string name, params QuestionsLine[] table) : this(name, (IEnumerable<QuestionsLine>)table)
        {
            //...
        }

        public static async Task<QuestionsTable> LoadAsync(string filePath, IObjectSerializer objectSerializer)
        {
            return await objectSerializer.DeserializeAsync<QuestionsTable>(filePath);
        }

        public async Task SaveAsync(string filePath, IObjectSerializer objectSerializer)
        {
            await objectSerializer.SerializeAsync(this, filePath);
        }

        public bool IsCompleted()
        {
            return TableLines.SelectMany(x => x.LineItems).All(x => x.IsClosed == true);
        }
    }
}
