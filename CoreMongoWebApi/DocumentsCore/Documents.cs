using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DocumentsCore
{

   //public class ByteArrayJsonConverter : JsonConverter<byte[]?>
   // {
   //     public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
   //     {
   //         short[] sByteArray = JsonSerializer.Deserialize<short[]>(ref reader);
   //         byte[] value = new byte[sByteArray.Length];
   //         for (int i = 0; i < sByteArray.Length; i++)
   //         {
   //             value[i] = (byte)sByteArray[i];
   //         }

   //         return value;
   //     }

   //     public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
   //     {
   //         writer.WriteStartArray();

   //         foreach (var val in value)
   //         {
   //             writer.WriteNumberValue(val);
   //         }

   //         writer.WriteEndArray();
   //     }
   // }

    public class Document
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }

        // [JsonConverter(typeof(ByteArrayJsonConverter))]
        // public byte[] FileContent { get; set; }
    }
}
