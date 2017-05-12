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

function VotingOption(name,description,imageURL) {
	var self = this;
	self.name = ko.observable(name);
	self.Description = ko.observable(description);
	self.ImageURL = ko.observable(imageURL);
};

function ViewModel() {
	var self = this;
	this.AvailableOptions = ko.observableArray([]);
	self.newOption = ko.observable();

	self.addOption = function () {
		self.AvailableOptions.push(new VotingOption({ title: this.newOption() }));
	}
	self.newOption("");

};

var viewModel = new ViewModel();

ko.applyBindings(viewModel);

//$('#myDiv').click(function () {
//    alert("Hi")
//});