var services = angular.module('services.rest',[]);

services.factory('expenseFactory', ['$http', function($http) {

    var urlBase = '/api/expenses';
    var functionFactory = {};

    functionFactory.getExpenses = function () {
        return $http.get(urlBase);
    };

    functionFactory.getExpense = function (id) {
        return $http.get(urlBase + '/' + id);
    };
    functionFactory.getExpensesWithPage = function(pagesize, page) {
        return $http.get(urlBase + '/' + pagesize + '/' + page);
    };

    functionFactory.getExpensesByCategoryWithPage = function(pagesize, page, categoryid) {
        return $http.get(urlBase + '/category' + categoryid +'/' +pagesize + '/' + page);
    };
    
    functionFactory.getExpenseBySearchTextWithPage=function(pagesize, page, searchtext) {
        return $http.get(urlBase + '/' + pagesize + '/' + page+'/'+searchtext);
    }

    functionFactory.insertExpense = function (expense) {
        return $http.post(urlBase, expense);
    };

    functionFactory.updateExpense = function (expense) {
        return $http.put(urlBase + '/' + expense.id, expense);
    };

    functionFactory.deleteExpense = function (id) {
        return $http.delete(urlBase + '/' + id);
    };


    return functionFactory;
}]);