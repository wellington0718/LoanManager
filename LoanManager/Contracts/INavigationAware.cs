﻿namespace LoanManager.Contracts;

public interface INavigationAware
{
    public void OnNavigatedTo(object parameter);


    public void OnNavigatedFrom();

}
