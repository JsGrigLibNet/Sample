
angular.module("myApp", [])
    .controller('PageCtrl', function ($scope, $rootScope, $timeout, $interval) {
        $scope.app = { siteUrl: window.location.href};
        $.ajax({
            type: "GET",
            url: "/account/app"
        }).done(function (data) {
            $timeout(function() {
                $scope.app= data;
                $scope.app.siteUrl = window.location.href;
                if (!$scope.app.isAuthenticated) {
                    window.location.href = "/login.html";
                }
            });
        });
    });

angular.module("myApp").directive('myEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });

                event.preventDefault();
            }
        });
    };
});
angular.element(function () {
    angular.bootstrap(document, ['myApp']);
});