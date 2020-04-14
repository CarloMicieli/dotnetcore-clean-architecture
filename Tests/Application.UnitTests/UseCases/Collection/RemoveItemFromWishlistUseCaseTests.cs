﻿using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class RemoveItemFromWishlistUseCaseTests : UseCaseTestHelper<RemoveItemFromWishlist, RemoveItemFromWishlistOutput, RemoveItemFromWishlistOutputPort>
    {
    }
}