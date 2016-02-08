app.directive('fixedTableHeaders', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                container = element.parentsUntil(attrs.fixedTableHeaders);
                element.stickyTableHeaders({ scrollableArea: container, "fixedOffset": 2 });
            }, 0);
        }
    }
}]);