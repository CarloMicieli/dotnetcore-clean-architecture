namespace TreniniDotNet.IntegrationTests.Catalog.V1.Scales.Responses
{
    public class PostScaleResponse
    {
        public SlugResponse Slug { set; get; }
    }

    public class SlugResponse
    {
        public string Value { set; get; }
    }
}
