var expenseManagerControllers = angular.module('ExpenseManagerModule', []);

expenseManagerControllers.controller('ItemListCtrl', ['$scope', '$uibModal', 'spinFactory', 'expenseFactory', 'uiblockFactory', '$timeout',
    function ($scope, $uibModal, spinFactory, expenseFactory, uiblockFactory, $timeout) {
        $scope.mySelections = [];
        $scope.filterOpts = {
            filterText: ""
        };

        $scope.expenses = [];
        $scope.pagingOptions = { pageSize: 10, currentPage: 1 };
        $scope.totalItems = 0;
        $scope.numPages = 0;

        $scope.testUi = function () {
            uiblockFactory.start();
        }

        $scope.pageChanged = function () {

            var spinner;
            if ($scope.filterOpts.filterText) {
                var filterText = $scope.filterOpts.filterText.toLowerCase();
                spinner = spinFactory.start('expenseList');
                expenseFactory.getExpenseBySearchTextWithPage($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, filterText)
                    .success(function (data) {
                        spinner.stop();;
                        $scope.expenses = data.expenses;
                        $scope.totalItems = data.totalItems;
                    }).error(function (data, status, headers, config) {
                        spinner.stop();
                        alertify.error(data.message);
                    });;

            } else {
                spinner = spinFactory.start('expenseList');
                expenseFactory.getExpensesWithPage($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage)
                    .success(function (data) {
                        spinner.stop();
                        $scope.expenses = data.expenses;
                        $scope.totalItems = data.totalItems;
                    }).error(function (data, status, headers, config) {
                        spinner.stop();
                        alertify.error(data.message);
                    });

            }
        };
        //Initial Load
        $scope.pageChanged();

        var filterTextTimeout;
        $scope.$watch('filterOpts.filterText', function (newVal, oldVal) {

            if (filterTextTimeout) $timeout.cancel(filterTextTimeout);
            filterTextTimeout = $timeout(function () {
                $scope.pagingOptions.currentPage = 1;
                $scope.pageChanged();
            }, 2000);

        }, true);


        // Add new item
        $scope.addNewItem = function () {
            var modalInstance = $uibModal.open({
                templateUrl: 'home/form',
                controller: 'ModalAddCtrl',
                resolve: {
                    items: function () {
                        return $scope.formData;
                    }
                }
            });
            modalInstance.result.then(function (formData) {
                console.log(formData);
                expenseFactory.insertExpense(formData)
                    .success(function (data, status, headers, config) {
                        alertify.success(data.message);
                        $scope.pageChanged();
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                        alertify.error("Server Error");
                    });
            });

        }


        $scope.deleteItem = function (id, title) {
            var closable = alertify.dialog('confirm').setting('closable');

            alertify.dialog('confirm')
              .set({
                  'title': 'Delete',
                  'labels': { ok: 'Delete', cancel: 'Cancel' },
                  'message': 'Do you want to delete item title : ' + title,
                  'onok': function () {
                      expenseFactory.deleteExpense(id)
                        .success(function (data, status, headers, config) {
                            alertify.success('Success delete');
                            $scope.pageChanged()

                        })
                         .error(function (data, status, headers, config) {
                              console.log(data);
                             alertify.error("Server Error");

                         });
                  },
                  'oncancel': function () {
                      alertify.message('Cancel');
                  }
              }).show();

        }


        $scope.editItem = function (id, item) {
            var modalInstance = $uibModal.open({
                templateUrl: 'home/form',
                controller: ['$scope','$uibModalInstance',function ($scope, $uibModalInstance) {
                    $scope.title = "Edit Item Id :" + item.id;
                    $scope.buttonTitle = "Update";
                    $scope.formData = {
                        id: item.id,
                        title: item.title,
                        date: new Date(item.date),
                        amount: item.amount,
                        description: item.description
                    };

                    $scope.cancel = function () {
                        $uibModalInstance.dismiss('cancel');
                    };
                    $scope.save = function () {
                        $uibModalInstance.close($scope.formData);
                    }
                    $scope.open = function () {
                        $scope.opened = true;
                    };
                }],
                resolve: {
                    items: function () {
                        return $scope.formData;
                    }
                }
            });
            modalInstance.result.then(function (formData) {
                expenseFactory.updateExpense(formData)
                            .success(function (data, status, headers, config) {
                                alertify.success(data.message);
                                $scope.pageChanged();
                            })
                            .error(function (data, status, headers, config) {
                                alertify.error(data.message);
                            });


            });
        }

    }]);