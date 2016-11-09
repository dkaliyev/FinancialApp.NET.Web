/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="labeleditor.ts" />
module LabelApplication {
    export class LabelCollection {
        constructor(private $scope: ng.IScope) {

        }

        sequence = [
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

    //LabelEditor.editorModule.controller("labelCollection", ["$scope", LabelCollection]);
}