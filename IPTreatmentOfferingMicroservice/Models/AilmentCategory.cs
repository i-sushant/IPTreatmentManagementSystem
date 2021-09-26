using System.Text.Json.Serialization;

namespace IPTreatmentOfferingMicroservice.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AilmentCategory
    {
        Orthopaedics,
        Urology
    }
}
