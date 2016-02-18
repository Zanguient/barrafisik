'use strict';

/**
 * Config for the router
 */
app.config(['$stateProvider', '$urlRouterProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$ocLazyLoadProvider', 'JS_REQUIRES', '$httpProvider',
function ($stateProvider, $urlRouterProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, $ocLazyLoadProvider, jsRequires, $httpProvider) {

    app.controller = $controllerProvider.register;
    app.directive = $compileProvider.directive;
    app.filter = $filterProvider.register;
    app.factory = $provide.factory;
    app.service = $provide.service;
    app.constant = $provide.constant;
    app.value = $provide.value;

    // LAZY MODULES

    $ocLazyLoadProvider.config({
        debug: false,
        events: true,
        modules: jsRequires.modules
    });

    // APPLICATION ROUTES
    // -----------------------------------
    // For any unmatched url, redirect to /app/dashboard
    $httpProvider.interceptors.push('authInterceptorService');
    $urlRouterProvider.otherwise("/app/dashboard");
    //
    // Set up the states
    $stateProvider.state('app', {
        url: "/app",
        templateUrl: "assets/views/app.html",
        resolve: loadSequence('modernizr', 'moment', 'angularMoment', 'uiSwitch', 'perfect-scrollbar-plugin', 'toaster', 'ngAside', 'vAccordion', 'sweet-alert', 'chartjs', 'tc.chartjs', 'oitozero.ngSweetAlert', 'chatCtrl'),
        abstract: true
    }).state('app.dashboard', {
        url: "/dashboard",
        templateUrl: "app/views/dashboard/dashboard.html",
        resolve: loadSequence('jquery-sparkline', 'dashboardCtrl', 'clienteData', 'ngTable', 'clienteFilters', 'despesasData'),
        title: 'Dashboard',
        ncyBreadcrumb: {
            label: 'Dashboard'
        }
    })

        // Login routes
        .state('login', {
            url: '/login',
            template: '<div ui-view class="fade-in-right-big smooth"></div>',
            abstract: true
        }).state('login.signin', {
            url: '/signin',
            templateUrl: "app/views/login/login.html",
            resolve: loadSequence('loginCtrl'),
            controller: "loginCtrl as vm",
        })

        //Clientes
        .state('app.clientes', {
            url: '/clientes',
            template: '<div ui-view class="fade-in-up"></div>',
            title: 'Clientes',
            ncyBreadcrumb: {
                label: 'Clientes'
            }
        }).state('app.clientes.listar', {
            url: '/listar',
            templateUrl: "app/views/cliente/clientes.html",
            title: 'Lista de Clientes',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('clienteCtrl', 'ngTable', 'clienteData', 'clienteFilters', 'touchspin-plugin', 'ui.select'),
            controller: "clienteCtrl as vm",
            ncyBreadcrumb: {
                label: 'Listar'
            }
        }).state('app.clientes.cadastrar', {
            url: '/cadastrar',
            templateUrl: "app/views/cliente/create.html",
            title: 'Cadastrar Clientes',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('clienteCreateCtrl', 'clienteData', 'horarioData', 'ui.mask', 'filaEsperaData'),
            controller: "clienteCreateCtrl as vm",
            ncyBreadcrumb: {
                label: 'Cadastrar'
            }
        }).state('app.clientes.editar', {
            url: '/editar/{id}',
            templateUrl: "app/views/cliente/edit.html",
            title: 'Editar Cliente',
            resolve: loadSequence('clienteEditCtrl', 'clienteData', 'horarioData', 'ui.mask'),
            controller: "clienteEditCtrl as vm",
            ncyBreadcrumb: {
                label: 'Editar'
            }
        }).state('app.clientes.perfil', {
            url: '/perfil/{id}',
            templateUrl: "app/views/cliente/clientePerfil.html",
            title: 'Perfil Cliente',
            resolve: loadSequence('clientePerfilCtrl', 'ngTable', 'clienteData', 'horarioData', 'clienteFilters', 'receitasData'),
            ncyBreadcrumb: {
                label: 'Perfil do Cliente'
            },
            controller: 'clientePerfilCtrl as vm'
        }).state('app.clientes.aniversariantes', {
            url: '/aniversariantes/{mes}',
            templateUrl: "app/views/cliente/aniversariantes.html",
            title: 'Editar Cliente',
            resolve: loadSequence('clienteAniversariantesCtrl', 'clienteData', 'xeditable'),
            ncyBreadcrumb: {
                label: 'Aniversariantes'
            },
            controller: 'clienteAniversariantesCtrl',
        })

        //Horários
        .state('app.horarios', {
            url: '/horarios',
            templateUrl: "app/views/horario/horario.html",
            title: 'Lista de Hor&aacute;rios',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('horarioCtrl', 'horarioData', 'xeditable'),
            controller: "horarioCtrl as vm",
            ncyBreadcrumb: {
                label: 'Lista de Hor&aacute;rios'
            }
        })

        //Fila de espera
        .state('app.filaEspera', {
            url: "/filaEspera",
            templateUrl: "app/views/filaespera/filaespera.html",
            resolve: loadSequence('filaEsperaCtrl', 'filaEsperaData', 'ngTable', 'clienteFilters', 'ui.mask'),
            title: 'Fila de Espera',
            ncyBreadcrumb: {
                label: 'Fila de Espera'
            },
            controller: 'filaEsperaCtrl as vm'
        })

        //Accounts
        .state('app.accounts', {
            url: "/accounts",
            templateUrl: "app/views/accounts/listaAccounts.html",
            resolve: loadSequence('accountCtrl', 'accountsData', 'ngTable'),
            title: 'Lista de Acessos',
            ncyBreadcrumb: {
                label: 'Lista de Acessos'
            },
            controller: 'accountCtrl as vm'
        })


         //Financeiro
        .state('app.financeiro', {
            url: '/financeiro',
            template: '<div ui-view class="fade-in-up"></div>',
            title: 'Financeiro',
            ncyBreadcrumb: {
                label: 'Financeiro'
            }
        }).state('app.financeiro.receitas', {
            url: '/receitas',
            templateUrl: "app/views/receitas/receitas.html",
            title: 'Lista das Receitas',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('receitasCtrl', 'ngTable', 'receitasData', 'categoriaFinanceiraData', 'tipoPagamentoData', 'funcionariosData', 'clienteData'),
            controller: "receitasCtrl as vm",
            ncyBreadcrumb: {
                label: 'Receitas'
            }
        }).state('app.financeiro.despesas', {
            url: '/despesas',
            templateUrl: "app/views/despesas/despesas.html",
            title: 'Lista das Despesas',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('despesasCtrl', 'ngTable', 'despesasData', 'categoriaFinanceiraData', 'tipoPagamentoData', 'funcionariosData', 'fornecedoresData'),
            controller: "despesasCtrl as vm",
            ncyBreadcrumb: {
                label: 'Despesas'
            }
        }).state('app.financeiro.categorias', {
            url: '/categorias',
            templateUrl: "app/views/categoriaFinanceira/categoriaFinanceira.html",
            title: 'Categorias Financeiras',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('categoriaFinanceiraCtrl', 'ngTable', 'categoriaFinanceiraData'),
            controller: "categoriaFinanceiraCtrl as vm",
            ncyBreadcrumb: {
                label: 'Categorias'
            }
        }).state('app.financeiro.tipoPagamento', {
            url: '/tipoPagamento',
            templateUrl: "app/views/tipoPagamento/tipoPagamento.html",
            title: 'Tipos de Pagamento',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('tipoPagamentoCtrl', 'ngTable', 'tipoPagamentoData'),
            controller: "tipoPagamentoCtrl as vm",
            ncyBreadcrumb: {
                label: 'Tipos de Pagamento'
            }
        })


        //Relatórios financeiros
        .state('app.financeiro.relatorioFinanceiro', {
            url: '/relatoriofinanceiro',
            templateUrl: "app/views/relatorioFinanceiro/relatorioFinanceiro.html",
            title: 'Relatorio Financeiro - Geral',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('relatorioFinanceiroCtrl', 'ngTable', 'relatorioFinanceiroData', 'categoriaFinanceiraData', 'subCategoriaData'),
            controller: "relatorioFinanceiroCtrl as vm",
            ncyBreadcrumb: {
                label: 'Relatorio Financeiro'
            }
        })

        .state('app.financeiro.relatorioFinanceiroReceitas', {
            url: '/relatoriofinanceiroReceitas',
            templateUrl: "app/views/relatorioFinanceiro/relatorioFinanceiroReceitas.html",
            title: 'Relatorio Financeiro - Receitas',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('relatorioFinanceiroReceitasCtrl', 'ngTable', 'tipoPagamentoData', 'relatorioFinanceiroData', 'categoriaFinanceiraData', 'subCategoriaData', 'funcionariosData'),
            controller: "relatorioFinanceiroReceitasCtrl as vm",
            ncyBreadcrumb: {
                label: 'Relatorio Financeiro - Receitas'
            }
        })

        .state('app.financeiro.relatorioFinanceiroDespesas', {
            url: '/relatoriofinanceiroDespesas',
            templateUrl: "app/views/relatorioFinanceiro/relatorioFinanceiroDespesas.html",
            title: 'Relatorio Financeiro - Despesas',
            icon: 'ti-layout-media-left-alt',
            resolve: loadSequence('relatorioFinanceiroDespesasCtrl', 'ngTable', 'tipoPagamentoData', 'relatorioFinanceiroData', 'categoriaFinanceiraData', 'subCategoriaData', 'funcionariosData', 'fornecedoresData'),
            controller: "relatorioFinanceiroDespesasCtrl as vm",
            ncyBreadcrumb: {
                label: 'Relatorio Financeiro - Despesas'
            }
        })

        //CADASTROS BÁSICOS
        //Armazem
        .state('app.armazem', {
            url: "/armazem",
            templateUrl: "app/views/armazem/armazem.html",
            resolve: loadSequence('armazemCtrl', 'armazemData', 'ngTable', 'ui.mask'),
            title: 'Armaz&eacute;m',
            ncyBreadcrumb: {
                label: 'Armaz&eacute;m'
            },
            controller: 'armazemCtrl as vm'
        })
        //Funcionarios
        .state('app.funcionarios', {
            url: "/funcionarios",
            templateUrl: "app/views/funcionarios/funcionarios.html",
            resolve: loadSequence('funcionariosCtrl', 'funcionariosData', 'ngTable', 'ui.mask', 'clienteFilters'),
            title: 'Funcionarios',
            ncyBreadcrumb: {
                label: 'Funcionarios'
            },
            controller: 'funcionariosCtrl as vm'
        })
        //Fornecedores
        .state('app.fornecedores', {
            url: "/fornecedores",
            templateUrl: "app/views/fornecedores/fornecedores.html",
            resolve: loadSequence('fornecedoresCtrl', 'fornecedoresData', 'ngTable', 'ui.mask', 'clienteFilters'),
            title: 'Fornecedores',
            ncyBreadcrumb: {
                label: 'Fornecedores'
            },
            controller: 'fornecedoresCtrl as vm'
        })


        //ESTOQUE
        //Categoria dos Produtos        
        .state('app.produtosCategoria', {
            url: "/produtosCategoria",
            templateUrl: "app/views/produtosCategoria/produtosCategoria.html",
            resolve: loadSequence('produtosCategoriaCtrl', 'produtosCategoriaData', 'ngTable'),
            title: 'Categoria dos Produtos',
            ncyBreadcrumb: {
                label: 'Categoria dos Produtos'
            },
            controller: 'produtosCategoriaCtrl as vm'
        })
        .state('app.produtos', {
            url: "/produtos",
            templateUrl: "app/views/produtos/produtos.html",
            resolve: loadSequence('produtosCtrl', 'produtosData', 'produtosCategoriaData', 'fornecedoresData', 'ngTable'),
            title: 'Produtos',
            ncyBreadcrumb: {
                label: 'Produtos'
            },
            controller: 'produtosCtrl as vm'
        })
        .state('app.estoque', {
            url: "/estoque",
            templateUrl: "app/views/estoque/estoque.html",
            resolve: loadSequence('estoqueCtrl', 'estoqueData', 'movimentacaoData', 'ngTable'),
            title: 'Estoque',
            ncyBreadcrumb: {
                label: 'Estoque'
            },
            controller: 'estoqueCtrl as vm'
        })
        .state('app.movimentacao', {
            url: "/movimentacao",
            templateUrl: "app/views/movimentacaoEstoque/movimentacao.html",
            resolve: loadSequence('movimentacaoCtrl', 'movimentacaoData', 'fornecedoresData', 'ngTable'),
            title: 'Movimentacao Estoque',
            ncyBreadcrumb: {
                label: 'Movimentacao Estoque'
            },
            controller: 'movimentacaoCtrl as vm'
        })

        //Valores
     .state('app.valores', {
         url: "/valores",
         templateUrl: "app/views/valores/valores.html",
         resolve: loadSequence('valoresCtrl', 'valoresData', 'ngTable'),
         title: 'Lista de Valores',
         ncyBreadcrumb: {
             label: 'Lista de Valores'
         },
         controller: 'valoresCtrl as vm'
     });




    // Generates a resolve object previously configured in constant.JS_REQUIRES (config.constant.js)
    function loadSequence() {
        var _args = arguments;
        return {
            deps: ['$ocLazyLoad', '$q',
			function ($ocLL, $q) {
			    var promise = $q.when(1);
			    for (var i = 0, len = _args.length; i < len; i++) {
			        promise = promiseThen(_args[i]);
			    }
			    return promise;

			    function promiseThen(_arg) {
			        if (typeof _arg == 'function')
			            return promise.then(_arg);
			        else
			            return promise.then(function () {
			                var nowLoad = requiredData(_arg);
			                if (!nowLoad)
			                    return $.error('Route resolve: Bad resource name [' + _arg + ']');
			                return $ocLL.load(nowLoad);
			            });
			    }

			    function requiredData(name) {
			        if (jsRequires.modules)
			            for (var m in jsRequires.modules)
			                if (jsRequires.modules[m].name && jsRequires.modules[m].name === name)
			                    return jsRequires.modules[m];
			        return jsRequires.scripts && jsRequires.scripts[name];
			    }
			}]
        };
    }
}]);