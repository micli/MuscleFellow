angular.module('starter.controllers', [])

.controller('AppCtrl', function($rootScope, $scope, $http, $ionicModal, $timeout) {

  // With the new view caching in Ionic, Controllers are only called
  // when they are recreated or on app start, instead of every page change.
  // To listen for when this page is active (for example, to refresh data),
  // listen for the $ionicView.enter event:
  //$scope.$on('$ionicView.enter', function(e) {
  //});

    //$rootScope.ServiceUrl = "http://10.172.176.39:5000/api/v1/";
    //$rootScope.ImageRoot = "http://10.172.176.39:5000/";
    $rootScope.ServiceUrl = "http://localhost:16192/api/v1/";
    $rootScope.ImageRoot = "http://localhost:16192";
    $rootScope.accessToken = {};
  // Form data for the login modal
    $scope.loginData = {};
    $scope.loginSucceeded = false;

  // Create the login modal that we will use later
  $ionicModal.fromTemplateUrl('templates/login.html', {
    scope: $scope
  }).then(function(modal) {
    $scope.modal = modal;
  });

  // Triggered in the login modal to close it
  $scope.closeLogin = function() {
    $scope.modal.hide();
  };

  // Open the login modal
  $scope.login = function() {
    $scope.modal.show();
  };

  // Perform the login action when the user submits the login form
  $scope.doLogin = function() {
    console.log('Doing login', $scope.loginData);
      //<> 登录逻辑代码
    var req = {
        method: 'POST',
        url: $scope.ServiceUrl + "Account/Login",
        headers: {
            'Content-Type' : 'application/json'
        },
        data: JSON.stringify($scope.loginData)
    };
    $http(req).then(
        function successCallback(response) {
            // login succeeded.
            if (undefined === response.data)
                return;
            $rootScope.accessToken = response.data;
            $http.defaults.headers.common.Authorization = 'Bearer ' + $rootScope.accessToken.access_token;
            $scope.loginSucceeded = true;
        },
    function errorCallback(response) {
        $rootScope.accessToken = {};
        $scope.loginSucceeded = false;
        alert("登录失败,请重新输入用户名密码")
    });

    // Simulate a login delay. Remove this and replace with your login
    // code if using a login system
    $timeout(function() {
      $scope.closeLogin();
    }, 1000);
  };
})

.controller('ProductListCtrl', function ($rootScope, $scope, $http) {
    var req = {
        method: 'GET',
        url: $rootScope.ServiceUrl + "Products?pagesize=50&page=0",
    };
    $http(req).then(
        function successCallback(response) {
            $rootScope.Products = angular.fromJson(response.data);
        },
    function errorCallback(response) {
        // var test = response.data;
        alert("数据加载失败, 请检查网络连接");
    });

})
.controller('ProductDetailsCtrl', function ($rootScope, $scope, $http, $stateParams) {
    var req = {
        method: 'GET',
        url: $rootScope.ServiceUrl + "Products/" + $stateParams.ProductID,
    };
    $http(req).then(
        function successCallback(response) {
            $scope.productData = angular.fromJson(response.data);
        },
    function errorCallback(response) {
        // var test = response.data;
        alert("数据加载失败, 请检查网络连接");
    });
    
    $scope.OnSubmitOrder = function(productID) {
        if (false === $scope.loginSucceeded) {
            alert("请先登录");
            return;
        }
        var data = { "UserID": $scope.loginData.userID, "ProductID": productID, "Amount": 1 };
        var req = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $rootScope.accessToken.access_token
            },
            data: data,
            url: $rootScope.ServiceUrl + "Orders",
        };
        $http(req).then(
            function successCallback(response) {
                alert("商品已经成功下单");
            },
            function errorCallback(response) {
                if (response.status == 403)
                    alert("请先登录");
                else
                    alert("下订单失败");
            });
    };
    $scope.OnAddShoppingCart = function (productID) {
        if (false === $scope.loginSucceeded) {
            alert("请先登录");
            return;
        }
        var data = { "UserID":$scope.loginData.userID, "ProductID" : productID, "Amount" : 1};
        var req = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $rootScope.accessToken.access_token
            },
            data: data,
            url: $rootScope.ServiceUrl + "ShoppingCart",
        };
        $http(req).then(
            function successCallback(response) {
                alert("商品已成功加入购物车");
            },
            function errorCallback(response) {
                if (response.status == 403)
                    alert("请先登录");
            });
    };
})
.controller('ShoppingCartCtrl', function ($rootScope, $scope, $http) {
    if (false === $scope.loginSucceeded) {
        alert("请先登录");
        return;
    }
    var req = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + $rootScope.accessToken.access_token
        },
        url: $rootScope.ServiceUrl + "ShoppingCart?" + 'userID=' + $scope.loginData.userID + '&pagesize=50&page=0',
    }
    $http(req).then(
        function successCallback(response) {
            $scope.CartItems = angular.fromJson(response.data);
        },
    function errorCallback(response) {
        // var test = response.data;
        alert("数据加载失败, 请检查网络连接");
    });
})
;


