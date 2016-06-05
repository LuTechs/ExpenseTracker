var app = angular.module('services.uiblock',[]);

app.factory('uiblockFactory', ['$uibModal',function($uibModal) {
    var modalInstance;
    var uiblockFuncs= {
        'start':function() {
            modalInstance = $uibModal.open({
                template: '<div><i class="fa fa-spinner fa-spin fa-5x"></i> <span>Loading...</span></div>',
                controller: 'ModalInstanceCtrl',
                windowClass: 'center-modal',
                backdrop: 'static',
                keyboard: false,
                size:'sm'
        });

        },
        'stop':function() {
            modalInstance.dismiss('cancel');
        }
    }
    return uiblockFuncs;
}]);

app.controller('ModalInstanceCtrl',function($scope) {
    
})