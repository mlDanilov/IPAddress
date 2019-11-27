'use strict';
ipApp.factory('Clients', function ($http) {


    let _currentClient = null;

    let getPOSTurl = function () {
        let args = _currentClient;
        if (args.id == undefined)
            args.id = -1;
        //delete args.ipAddress;
        let url = "/api/Client?Id=" + args.id
            + "&FirstName=" + args.firstName
            + "&SecondName=" + args.secondName
            + "&Patronymic=" + args.patronymic
            + "&BirthDay=" + args.birthDay.toISOString()
            + "&sex=" + args.sex
          
        return url;
    }
    let getPUTurl = function () {
        let args = _currentClient;
        //delete args.ipAddress;
        let url = "/api/Client?Id=" + args.id
            + "&FirstName=" + args.firstName
            + "&SecondName=" + args.secondName
            + "&Patronymic=" + args.patronymic
            + "&BirthDay=" + args.birthDay.toISOString()
            + "&sex=" + args.sex
        return url;
    }

    return {

        //Установить текущего клиента
        setCurrentClient: function (client_) {
            _currentClient = client_;
        },

        //Получить список групп
        getAll: function () {
            return $http({
                method: 'GET',
                url: "/api/Client/"
                , headers: {
                    'Content-Type': 'application/json'
                }
            });
        },

        getVoidClient: function () {
            return {
                id: null,
                firstName: null,
                secondName: null,
                patronymic: null,
                birthDay: null,
                sex: null
            }
        },

        create: function () {
            return $http({
                method: 'POST',
                url: getPOSTurl()
            });
        },

        edit: function () {
            return $http({
                method: 'PUT',
                url: getPUTurl()
            });
        },

        delete: function () {
            return $http({
                method: 'DELETE',
                url: "/api/Client/" + _currentClient.id
            });
        }
        
    }
});