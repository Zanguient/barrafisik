'use strict';
app.directive('xngFocus', function () {
      return function (scope, element, attrs) {
          scope.$watch(attrs.xngFocus,
            function (newValue) {
                newValue && element.focus();
            }, true);
      };
  });