using System;
using Newtonsoft.Json;

namespace IntelligentPx.Models
{
    public class Photo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("camera")]
        public string Camera { get; set; }

        [JsonProperty("lens")]
        public string Lens { get; set; }

        [JsonProperty("focal_length")]
        public string FocalLength { get; set; }

        [JsonProperty("iso")]
        public string Iso { get; set; }

        [JsonProperty("shutter_speed")]
        public string ShutterSpeed { get; set; }

        [JsonProperty("aperture")]
        public string Aperture { get; set; }

        [JsonProperty("times_viewed")]
        public int TimesViewed { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("category")]
        public int Category { get; set; }

        [JsonProperty("location")]
        public object Location { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("taken_at")]
        public string TakenAt { get; set; }

        [JsonProperty("hi_res_uploaded")]
        public int HiResUploaded { get; set; }

        [JsonProperty("for_sale")]
        public bool ForSale { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("votes_count")]
        public int VotesCount { get; set; }

        [JsonProperty("favorites_count")]
        public int FavoritesCount { get; set; }

        [JsonProperty("comments_count")]
        public int CommentsCount { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("sales_count")]
        public int SalesCount { get; set; }

        [JsonProperty("for_sale_date")]
        public object ForSaleDate { get; set; }

        [JsonProperty("highest_rating")]
        public double HighestRating { get; set; }

        [JsonProperty("highest_rating_date")]
        public string HighestRatingDate { get; set; }

        [JsonProperty("license_type")]
        public int LicenseType { get; set; }

        [JsonProperty("converted")]
        public int Converted { get; set; }

        [JsonProperty("collections_count")]
        public int CollectionsCount { get; set; }

        [JsonProperty("crop_version")]
        public int CropVersion { get; set; }

        [JsonProperty("privacy")]
        public bool Privacy { get; set; }

        [JsonProperty("profile")]
        public bool Profile { get; set; }

        [JsonProperty("for_critique")]
        public bool ForCritique { get; set; }

        [JsonProperty("images")]
        public ImageCollection Images { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("positive_votes_count")]
        public int PositiveVotesCount { get; set; }

        [JsonProperty("converted_bits")]
        public int ConvertedBits { get; set; }

        [JsonProperty("watermark")]
        public bool Watermark { get; set; }

        [JsonProperty("image_format")]
        public string ImageFormat { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("licensing_requested")]
        public bool LicensingRequested { get; set; }

        [JsonProperty("licensing_suggested")]
        public bool LicensingSuggested { get; set; }

        [JsonProperty("is_free_photo")]
        public bool IsFreePhoto { get; set; }

        [JsonIgnore]
        public string CopyrightNotice => $"© {User.FullName} / 500px";

        [JsonIgnore]
        public string FullUrl => $"https://500px.com{Url}";
    }
}