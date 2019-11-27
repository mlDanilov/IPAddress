'use strict'
var ipApp = angular.module('ipApp');
ipApp.controller("clientController", function (
    $scope, IPAddress) {

    //Создать у объектов "клиент"  свойство "ФИО"
    let addFIOToClients = function (data) {
        data.forEach(c => c.FIO = c.firstName + " " + c.secondName + " " + c.patronymic);
    }

    //Создать объектов "Подсеть"  свойство "IP адрес"
    let addIpAddressIpNets = function (data) {
        data.forEach(c => c.ipAddress = c.oktet1 + "." + c.oktet2 + "." + c.oktet3 + "." + c.oktet4 + "/" + c.mask);
    }
    //Обновить IpAddress
    let refreshIpAddress = function (ipNet) {
        let ipAddress = c.oktet1 + "." + c.oktet2 + "." + c.oktet3 + "." + c.oktet4 + "/" + c.mask
        ipNet.IPAddress = ipAddress;
    }

    //Загрузить IP подсети клиента
    let loadIPAddressesCurrentClient = function () {
        let promise = IPAddress.getByClientId();
        if (promise == null)
            return;

        promise.then(
            function successCallback(response) {
                console.log('loadIPAddressesCurrentClient success');
                addIpAddressIpNets(response.data);
                $scope.IPNets.Items = response.data;
            },
            function errorCallback(response) {
                console.log('loadIPAddressesCurrentClient error');
            },

        )
    }

    Clients.getAll().then(
        function successCallback(response) {
            console.log("Clients.getAll success");
            addFIOToClients(response.data);
            $scope.Clients.Items = response.data;
            $scope.$watch('Clients.SelectedItem', loadIPAddressesCurrentClient);
        },
        function errorCallback(response) {
            console.log("Clients.getAll error");
        },
    );
    //текущий клиент изменился
    $scope.currentClientChanged = function () {
        console.log('currentClientChanged');
        $scope.ipNet_edit = null;
        if ($scope.Clients.SelectedItem != null)
            IPAddress.setCurrentClient($scope.Clients.SelectedItem);
    }

    //текущий клиент изменился
    $scope.currentIPNetChanged = function () {
        console.log('currentIPNetChanged');
        if ($scope.IPNets.SelectedItem != null) {
            IPAddress.setCurrentClientIpNet($scope.IPNets.SelectedItem);
            $scope.ipNet_edit = $scope.IPNets.SelectedItem;

        }


    }


    //Клиенты
    $scope.Clients = {
        SelectedItem: null,
        Items: []
    };

    //Ip-подсети
    $scope.IPNets = {
        SelectedItem: null,
        Items: []
    };

    $scope.ipNet_edit = null;

    //Добавить новую подсеть клиенту
    $scope.addIpNet = function () {
        let promise = IPAddress.addIpNetToClient();
        if (promise == null)
            return;
        promise.then(
            function successCallback(response) {
                console.log('addIpNet success');
                //let ipnetRes = $scope.IPNets.Items.find(
                //    ipnet => ipnet.id == $scope.ipNet_edit.id);
                //refreshIpAddress(ipnetRes);
            },
            function errorCallback(response) {
                console.log('addIpNet error');
            });
    }

    //Редактировать подсеть
    $scope.editIpNet = function () {
        let promise = IPAddress.changeCurrentIPnet();
        if (promise == null)
            return;
        promise.then(
            function successCallback(response) {
                console.log('editIpNet success');
                //let ipnetRes = $scope.IPNets.Items.find(
                //    ipnet => ipnet.id == $scope.ipNet_edit.id);
                //refreshIpAddress(ipnetRes);
            },
            function errorCallback(response) {
                console.log('editIpNet error');
            });
    }

    //Удалить подсеть у пользователя
    $scope.deleteIpNetFromClient = function () {
        let promise = IPAddress.deleteIpNetFromClient();
        promise.then(
            function successCallback(data) {
                console.log('deleteIpNetFromClient success');
            },
            function errorCallback(data) {
                console.log('deleteIpNetFromClient error');
            });
    }

});