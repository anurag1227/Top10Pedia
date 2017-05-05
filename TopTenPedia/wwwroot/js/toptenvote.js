///// <reference path ="../lib/knockout/dist/knockout.js"/>

//$(function () {
//    var data = [{ name: "anurag" }, { name: "AbhiShek" }, { name: "ANimesh" }]
//    var viewmodel = {
//        list: ko.observableArray(data),
//        additem: function () {
//            this.list.push({ name: "anika" });
//        },
//        removeitem: function () {
//            this.list.pop();
//        }
//    }
//    ko.applyBindings(viewmodel);
//});

function ViewModel() {

	this.name = 'Eric McQuiggan';

};

var viewModel = new ViewModel();

ko.applyBindings(viewModel);

//$('#myDiv').click(function () {
//    alert("Hi")
//});