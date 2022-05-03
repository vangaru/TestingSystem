﻿using Tests.Api.Models;

namespace Tests.Api.Interfaces;

public interface ITestsInfoProvider
{
    public Task CreateTest(CreateTestModel createTestModel, string testCreatorName);

    public Task<IEnumerable<CreatedTestsGridItem>> GetCreatedTestsInfo(string testCreatorName);
}