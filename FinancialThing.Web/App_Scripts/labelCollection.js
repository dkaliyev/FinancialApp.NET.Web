/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="labeleditor.ts" />
var LabelApplication;
(function (LabelApplication) {
    var LabelCollection = (function () {
        function LabelCollection($scope) {
            this.$scope = $scope;
            this.sequence = [
                {
                    Color: "red",
                    Text: "Red"
                },
                {
                    Color: "Green",
                    Text: "Green"
                },
                {
                    Color: "Blue",
                    Text: "Blue"
                }];
        }
        return LabelCollection;
    }());
    LabelApplication.LabelCollection = LabelCollection;
})(LabelApplication || (LabelApplication = {}));
//# sourceMappingURL=labelCollection.js.map