﻿using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WPF.Services.Serialization
{
    public class TwoDimStringArrayJsonConverter : JsonConverter<string[,]>
    {
        public override string[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);

            var rowLength = jsonDoc.RootElement.GetArrayLength();
            var columnLength = jsonDoc.RootElement.EnumerateArray().First().GetArrayLength();

            var grid = new string[rowLength, columnLength];

            int row = 0;
            foreach (var array in jsonDoc.RootElement.EnumerateArray())
            {
                int column = 0;
                foreach (var number in array.EnumerateArray())
                {
                    grid[row, column] = number.GetString();
                    column++;
                }
                row++;
            }

            return grid;
        }

        public override void Write(Utf8JsonWriter writer, string[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            for (int i = 0; i < value.GetLength(0); i++)
            {
                writer.WriteStartArray();
                for (int j = 0; j < value.GetLength(1); j++)
                {
                    writer.WriteStringValue(value[i, j]);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }
}
