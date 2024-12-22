using System.Text.Json;
using System.Text.Json.Serialization;

public class DictionaryValueTupleConverter<TValue> : JsonConverter<Dictionary<(string, int), TValue>>
{
    public override Dictionary<(string, int), TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dictionary = new Dictionary<(string, int), TValue>();

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token.");
        }

        reader.Read(); 
        while (reader.TokenType != JsonTokenType.EndObject)
        {
            string keyString = reader.GetString();
            var keyParts = keyString.Split(','); 
            var key = (keyParts[0], int.Parse(keyParts[1]));

            reader.Read(); 
            TValue value = JsonSerializer.Deserialize<TValue>(ref reader, options);

            dictionary[key] = value;

            reader.Read(); 
        }

        return dictionary;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<(string, int), TValue> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value)
        {

            string keyString = $"{kvp.Key.Item1},{kvp.Key.Item2}";
            writer.WritePropertyName(keyString);

            JsonSerializer.Serialize(writer, kvp.Value, options);
        }

        writer.WriteEndObject();
    }
}