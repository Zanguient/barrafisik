(function () {
    "use strict";

    app.controller('movimentacaoCtrl', movimentacaoCtrl);

    function movimentacaoCtrl($scope, ngTableParams, movimentacaoData, fornecedoresData, SweetAlert, $filter, $state, $modal, $timeout) {
        var vm = this;
        vm.movimentacao = [];

        $scope.grupo = 'Armazem.Descricao';

        activate();

        function activate() {
            $scope.$emit('LOAD');
            movimentacaoData.getAll().then(function (result) {
                vm.movimentacao = result.data;
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 40,          // count per page
            sorting: {
                Produto: 'asc'     // initial sorting
            },
        }, {
            counts: [],
            total: vm.movimentacao.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.movimentacao, params.orderBy()) : vm.movimentacao;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.movimentacao', function () {
            $scope.tableParams.reload();
        });

        //List Fornecedores
        fornecedoresData.getAllAtivos().then(function (fornecedoresList) {
            $scope.fornecedores = fornecedoresList.data;
        });

        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/estoque/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/estoque/estoqueCreateCtrl.js', 'ui.mask', 'app/factory/fornecedoresData.js', 'app/factory/produtosCategoriaData.js', 'app/factory/produtosData.js', 'app/factory/armazemData.js']);
                        }
                    ]
                },
                controller: 'estoqueCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Estoque cadastrado com sucesso!", "success");
                estoqueData.getAll().then(function (result) {
                    vm.estoques = result.data;
                });
            }, function () {
                console.log('Cancelled');
            });
        }       

        $scope.formEdit = {
            submit: function (form, movimentacao) {
                var firstError = null;
                if (form.$invalid) {

                    var field = null, firstError = null;
                    for (field in form) {
                        if (field[0] != '$') {
                            if (firstError === null && !form[field].$valid) {
                                firstError = form[field].$name;
                            }

                            if (form[field].$pristine) {
                                form[field].$dirty = true;
                            }
                        }
                    }
                    angular.element('.ng-invalid[name=' + firstError + ']').focus();
                    return;

                } else {
                    movimentacaoData.edit(movimentacao).success(function () {
                        SweetAlert.swal("Atualizado!", "Dados salvos com sucesso!", "success");
                        $scope.editId = -1;
                        movimentacaoData.getAll().then(function (result) {
                            vm.movimentacao = result.data;
                        });
                    }).error(function (error) {
                        SweetAlert.swal("Erro!", error.Message, "error");
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao salvar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });
                }
            }
        };

        $scope.delete = function(id) {
            SweetAlert.swal({
                title: "Confirmar Exclusão?",
                text: "Tem certeza que deseja excluir esse registro?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: true
            }, function(isConfirm) {
                if (isConfirm) {
                    movimentacaoData.remove(id).success(function() {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.movimentacao, function(i) {
                            if (vm.movimentacao[i].MovimentacaoId === id) {
                                vm.movimentacao.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    }).error(function(error) {
                        SweetAlert.swal("Erro!", error.Message, "error");
                    });
                }
            });
        }

        $scope.editId = -1;

            $scope.setEditId = function (pid) {
                $scope.editId = pid;
            };

            $scope.opened = [];
            $scope.open = function ($event, index) {
                $event.preventDefault();
                $event.stopPropagation();
                $timeout(function () {
                    $scope.opened[index] = true;
                });
            };

            $scope.dateOptions = {
                'year-format': "'yyyy'",
                'starting-day': 1
            };
        }
    })();