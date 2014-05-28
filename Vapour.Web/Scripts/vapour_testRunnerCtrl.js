/*global console:false, angular:false */
var app = angular.module('vapour', [])
  .config(function ( $httpProvider) {
      delete $httpProvider.defaults.headers.common['X-Requested-With'];
  });


app.controller('testRunnerCtrl', function($scope, $http, $filter) {
  var self = this;

  $scope.loading = true;

  $scope.initialize = function(apiUrl) {

    $http.jsonp(apiUrl + '?callback=JSON_CALLBACK').
      success(function(data, status, headers, config) {
        $scope.loading = false;

        $scope.results = {
          success: data.Success,
          content: data.Message,
          failedTests: data.FailedTests
        };
      }).
      error(function(data, status, headers, config) {
        console.log('Error: ' + data + ', Status: ' + status);
      });
  };
});