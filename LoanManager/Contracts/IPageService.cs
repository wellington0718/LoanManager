using System;

namespace LoanManager.Contracts;

public interface IPageService
{
    Type GetPageType(string key);
}
