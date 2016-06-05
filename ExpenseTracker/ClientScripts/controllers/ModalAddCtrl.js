var modalController = angular.module('modalModule', []);

modalController.controller('ModalAddCtrl', [
    '$scope', '$uibModalInstance', function ($scope,  $uibModalInstance) {
        $scope.title = "Add new item";
        $scope.buttonTitle = "Add New Item";
        $scope.formData = {};
        $scope.formData.date = moment().format("DD-MMMM-YYYY");

        $scope.formData.categoryId = 19;
        $scope.cancel = function () {
             $uibModalInstance.dismiss('cancel');
        };
        $scope.save = function () {
            $scope.formData.date = $scope.formData.date.toISOString();
             $uibModalInstance.close($scope.formData);
        }
        $scope.open = function () {
            $scope.opened = true;
        };
    }
]);