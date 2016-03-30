//immediately invoked function expression
(function () {
    "use strict";
    angular.module("simpleControls", [])
        .directive("waitCursor",waitCursor);

    function waitCursor() {
        return {
            scope: {
                show:"=displayWhen"
            },
            restrict:"E", //restrict to only the element style
            templateUrl:"/views/waitCursor.html"
        };
    }
})();