

namespace NPE.Core.Modules.Laboratory.TestHistory.DTOs
{
    public class TestHistoryRawResponse
    {
        public List<TestHistoryCaseDto> Cases { get; set; } = new();
        public List<TestHistoryTestDto> Tests { get; set; } = new();
    }
}