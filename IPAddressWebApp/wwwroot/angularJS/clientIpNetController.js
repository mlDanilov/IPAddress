'use strict'
var ipApp = angular.module('ipApp');
ipApp.controller("clientIpNetController", function (
    $scope, Clients, IPAddress, $timeout)
{

    //Создать у объектов "клиент"  свойство "ФИО"
    let addFIOToClients = function (data) {
        data.forEach(c => updateFIO(c));
        data.forEach(c => c.birthDay = new Date(c.birthDay));
    }

    let updateFIO = function (client) {
        client.FIO = client.firstName + " " + client.secondName + " " + client.patronymic;
    }

    //Создать объектов "Подсеть"  свойство "IP адрес"
    let addIpAddressIpNets = function (data) {
        data.forEach(c => c.ipAddress = c.oktet1 + "." + c.oktet2 + "." + c.oktet3 + "." + c.oktet4 + "/" + c.mask);
    }

    let updateIPAddress = function (net) {
        net.ipAddress = net.oktet1 + "." + net.oktet2 + "." + net.oktet3 + "." + net.oktet4 + "/" + net.mask;
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

    let realoadClients = function () {
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
        $scope.client_edit = null;
    }


    let eraseError = function () {
        $scope.error = null;
    }
    //Отобразить сообщение об ошибке на 10 сек и почистить
    let showError = function (message) {
       
        $scope.error = message;
        let promise = $timeout(eraseError, 10000);

        promise.then(eraseError, eraseError);
    }
    

    realoadClients();

    

    //текущий клиент сменился
    $scope.currentClientChanged = function () {
        console.log('currentClientChanged');
        $scope.ipNet_edit = null;

        if ($scope.Clients.SelectedItem != null) {
            $scope.client_edit = Object.assign({}, $scope.Clients.SelectedItem);
            IPAddress.setCurrentClient($scope.client_edit);

            $scope.ipNet_edit = IPAddress.getVoidIpNet();
            $scope.ipNet_edit.clientId = $scope.client_edit.id;
            //Clients.setCurrentClient($scope.Clients.SelectedItem);
            //$scope.client_edit = $scope.Clients.SelectedItem;
            
        }

        
    }


    //текущая подсеть клиента сменилась
    $scope.currentIPNetChanged = function () {
        console.log('currentIPNetChanged');
        if ($scope.IPNets.SelectedItem != null) {
            IPAddress.setCurrentClientIpNet($scope.IPNets.SelectedItem);
            $scope.ipNet_edit = $scope.IPNets.SelectedItem;
            //$scope.ipNet_edit = Object.assign({}, $scope.IPNets.SelectedItem);

        }

        
    }
    
    //Пол
    $scope.Sex = [
        { id: 0, Name: 'Женщина' },
        { id: 1, Name: 'Мужчина' },
    ]

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

    //$scope.ipNet_edit = null;
    $scope.ipNet_edit = IPAddress.getVoidIpNet();

    $scope.client_edit = Clients.getVoidClient();

    //Добавить новую подсеть клиенту
    $scope.addIpNet = function () {
        IPAddress.setCurrentClientIpNet($scope.ipNet_edit);

        let promise = IPAddress.addIpNetToClient();
        if (promise == null)
            return;
        promise.then(
            function successCallback(response) {
                console.log('addIpNet success');
                loadIPAddressesCurrentClient();
                //let ipnetRes = $scope.IPNets.Items.find(
                //    ipnet => ipnet.id == $scope.ipNet_edit.id);
                //refreshIpAddress(ipnetRes);
            },
            function errorCallback(response) {
                console.log('addIpNet error');
                showError(response.data);
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
                //Обновляем отредактированного пользователя
                $scope.IPNets.Items.forEach(net => {
                    if (net.id == $scope.ipNet_edit.id) {
                        net.oktet1 = $scope.ipNet_edit.oktet1;
                        net.oktet2 = $scope.ipNet_edit.oktet2;
                        net.oktet3 = $scope.ipNet_edit.oktet3;
                        net.oktet4 = $scope.ipNet_edit.oktet4;
                        updateIPAddress(net);
                    }
                });
            },
            function errorCallback(response) {
                console.log('editIpNet error');
                showError(response.data);
            });
    }

    //Удалить подсеть у пользователя
    $scope.deleteIpNetFromClient = function () {
        let promise = IPAddress.deleteIpNetFromClient();
        promise.then(
            function successCallback(data) {
                loadIPAddressesCurrentClient();
                console.log('deleteIpNetFromClient success');
            },
            function errorCallback(data) {
                console.log('deleteIpNetFromClient error');
                showError(response.data);
            });
    }


    //Добавить нового клиента
    $scope.addClient = function () {
        Clients.setCurrentClient($scope.client_edit);

        let promise = Clients.create();
        if (promise == null)
            return;
        promise.then(
            function successCallback(response) {
                console.log('addClient success');
                realoadClients();
            },
            function errorCallback(response) {
                console.log('addClient error');
                showError(response.data);
            });
    }

    //Редактировать клиента
    $scope.editClient = function () {
        Clients.setCurrentClient($scope.client_edit);

        let promise = Clients.edit();
        if (promise == null)
            return;
        promise.then(
            function successCallback(response) {
                console.log('editClient success');
                $scope.Clients.SelectedItem = $scope.client_edit;
                //Обновляем отредактированного пользователя
                $scope.Clients.Items.forEach(c =>
                {
                    if (c.id == $scope.client_edit.id) {
                        c.firstName = $scope.client_edit.firstName;
                        c.secondName = $scope.client_edit.secondName;
                        c.patronymic = $scope.client_edit.patronymic;
                        c.birthDay = $scope.client_edit.birthDay;
                        c.sex = $scope.client_edit.sex;
                        updateFIO(c);
                    }
                });
            },
            function errorCallback(response) {
                console.log('editClient error');
                showError(response.data);
            });
    }

    //Удалить клиента
    $scope.deleteClient = function () {
        Clients.setCurrentClient($scope.client_edit);

        let promise = Clients.delete();
        promise.then(
            function successCallback(data) {
                console.log('deleteIpNetFromClient success');
                realoadClients();
            },
            function errorCallback(data) {
                console.log('deleteIpNetFromClient error');
                showError(response.data);
            });
    }
    //Валидны ли данные клиента, введёные пользователем для добавления?
    $scope.ClientAddIsInvalid = function () {
        if ($scope.client_edit == null)
            return true;

        if (!$scope.client_edit.firstName ||
            !$scope.client_edit.secondName ||
            !$scope.client_edit.patronymic ||
            !$scope.client_edit.birthDay ||
            (!$scope.client_edit.sex && $scope.client_edit.sex != 0)
            )
            return true;
        return false;
    }

    //Валидны ли данные клиента, введёные пользователем?
    $scope.ClientIsInvalid = function () {
        if ($scope.client_edit == null)
            return true;

        if (
            (!$scope.client_edit.id || $scope.client_edit.id == -1) ||
            !$scope.client_edit.firstName ||
            !$scope.client_edit.secondName ||
            !$scope.client_edit.patronymic ||
            !$scope.client_edit.birthDay ||
            (!$scope.client_edit.sex && $scope.client_edit.sex != 0)
        )
            return true;
        return false;
    }

    //Валидны ли данные подсети клиента, введёные пользователем для добавления?
    $scope.IpNetIsInvalidAdd = function () {
        if ($scope.ipNet_edit == null)
            return true;
        let id = $scope.ipNet_edit.id;
        let clientId = $scope.ipNet_edit.clientId;
        let oktet1 = $scope.ipNet_edit.oktet1;
        let oktet2 = $scope.ipNet_edit.oktet2;
        let oktet3 = $scope.ipNet_edit.oktet3;
        let oktet4 = $scope.ipNet_edit.oktet4;
        let mask = $scope.ipNet_edit.mask;

        if (
            ((clientId == null) || (clientId == -1)) ||
            ((oktet1 == null) || (oktet1 < 0 || oktet1 > 255)) ||
            ((oktet2 == null) || (oktet2 < 0 || oktet2 > 255)) ||
            ((oktet3 == null) || (oktet3 < 0 || oktet3 > 255)) ||
            ((oktet4 == null) || (oktet4 < 0 || oktet4 > 255)) ||
            ((mask == null) || (mask < 0 || mask > 32)))
            return true;
        return false;
    }

    //Валидны ли данные , введёные пользователем?
    $scope.IpNetIsInvalid = function () {
        if ($scope.ipNet_edit == null)
            return true;
        let id = $scope.ipNet_edit.id;
        let clientId = $scope.ipNet_edit.clientId;
        let oktet1 = $scope.ipNet_edit.oktet1;
        let oktet2 = $scope.ipNet_edit.oktet2;
        let oktet3 = $scope.ipNet_edit.oktet3;
        let oktet4 = $scope.ipNet_edit.oktet4;
        let mask = $scope.ipNet_edit.mask;

        if (
            ((id == null) || (id == -1)) ||
            ((clientId == null) || (clientId == -1)) ||
            ((oktet1 == null) || (oktet1 < 0 || oktet1 > 255)) ||
            ((oktet2 == null) || (oktet2 < 0 || oktet2 > 255)) ||
            ((oktet3 == null) || (oktet3 < 0 || oktet3 > 255)) ||
            ((oktet4 == null) || (oktet4 < 0 || oktet4 > 255)) ||
             ((mask == null) || (mask < 0 || mask > 32)))
            return true;
        return false;
    }
   
    $scope.error = null;
});