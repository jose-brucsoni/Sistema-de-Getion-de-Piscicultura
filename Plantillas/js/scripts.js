(function () {
  var heroSlides = [
    {
      image: "recursos/Imagenes/VistaPanoramicaDeLasLagunasDeCrianza.jpg",
      title: "Bienvenido al sistema de <span>gestión de piscicultura</span> de “Lagos Andinos”.",
      subtitle:
        "Centraliza la trazabilidad de tus lotes, el monitoreo del agua y la alimentación diaria desde una única plataforma híbrida pensada para la región andina.",
      secondary: false
    },
    {
      image: "recursos/Imagenes/FondoDePescesAcumulados.jpg",
      title: "Control fino de lotes, biomasa y calidad del agua en un solo vistazo.",
      subtitle:
        "Visualiza el estado sanitario de cada estanque y toma decisiones informadas antes de que los problemas impacten tu producción.",
      secondary: true
    },
    {
      image: "recursos/Imagenes/VistaDeTrabajadorRecolectandoLosPesces(1).jpg",
      title: "Datos capturados en campo, decisiones estratégicas en oficina.",
      subtitle:
        "Técnicos registran información desde dispositivos móviles mientras supervisores analizan indicadores clave en tiempo real.",
      secondary: true
    }
  ];

  var currentHeroIndex = 0;
  var heroBgImage = document.getElementById("hero-bg-image");
  var heroTitle = document.getElementById("hero-title");
  var heroSubtitle = document.getElementById("hero-subtitle");

  function renderHero(index) {
    var slide = heroSlides[index];
    if (!slide || !heroBgImage || !heroTitle || !heroSubtitle) return;
    heroBgImage.src = slide.image;
    heroTitle.innerHTML = slide.title;
    heroSubtitle.textContent = slide.subtitle;

    if (slide.secondary) {
      heroTitle.classList.add("hero-title--secondary");
    } else {
      heroTitle.classList.remove("hero-title--secondary");
    }
  }

  var btnPrev = document.querySelector("[data-hero-prev]");
  var btnNext = document.querySelector("[data-hero-next]");

  if (btnPrev) {
    btnPrev.addEventListener("click", function () {
      currentHeroIndex = (currentHeroIndex - 1 + heroSlides.length) % heroSlides.length;
      renderHero(currentHeroIndex);
    });
  }

  if (btnNext) {
    btnNext.addEventListener("click", function () {
      currentHeroIndex = (currentHeroIndex + 1) % heroSlides.length;
      renderHero(currentHeroIndex);
    });
  }

  renderHero(currentHeroIndex);

  var yearSpan = document.getElementById("year");
  if (yearSpan) {
    yearSpan.textContent = new Date().getFullYear().toString();
  }

  function navigateToLogin() {
    window.location.href = "/Plantillas/html/Paginas/InicioDeSesion.html";
  }

  var loginButtons = document.querySelectorAll("[data-login]");
  loginButtons.forEach(function (btn) {
    btn.addEventListener("click", navigateToLogin);
  });

  document.querySelectorAll("[data-scroll]").forEach(function (btn) {
    btn.addEventListener("click", function () {
      var targetSelector = btn.getAttribute("data-scroll");
      if (!targetSelector) return;
      var target = document.querySelector(targetSelector);
      if (!target) return;
      window.scrollTo({
        top: target.offsetTop - 72,
        behavior: "smooth"
      });
    });
  });

  var form = document.getElementById("contact-form");
  if (form) {
    form.addEventListener("submit", function (e) {
      e.preventDefault();
      alert(
        "Gracias por tu interés. Esta es una maqueta visual; la integración con el backend se realizará más adelante."
      );
    });
  }

  var mainContent = document.getElementById("main-content");
  if (mainContent) {
    var viewMap = {
      "lotes": "../Componentes/Lotes.html",
      "crianza": "../Componentes/Crianza.html",
      "cosechas": "../Componentes/Cosechas.html",
      "usuarios-roles": "../Componentes/Usuarios_Y_Roles.html",
      "estanques": "../Componentes/Estanques.html",
      "especies": "../Componentes/Especies.html",
      "proveedores": "../Componentes/Proveedores.html",
      "parametros-agua": "../Componentes/ParametrosDeAgua.html",
      "alimentacion": "../Componentes/Alimentacion.html",
      "seguimiento-alimentacion-e-inventario": "../Componentes/Seguimiento_Alimentacion_e_Inventario.html"
    };

    document.querySelectorAll(".sidebar-link[data-view]").forEach(function (link) {
      link.addEventListener("click", function (e) {
        e.preventDefault();
        var view = link.getAttribute("data-view");
        var url = viewMap[view];
        if (!url || !mainContent) return;

        fetch(url)
          .then(function (response) {
            if (!response.ok) throw new Error("Error al cargar el contenido.");
            return response.text();
          })
          .then(function (html) {
            mainContent.innerHTML = html;
            initLotesTabs();
            initCrianzaTabs();
            initCosechasTabs();
            initUserTabs();
            initEstanquesTabs();
            initEspeciesTabs();
            initProveedoresTabs();
            initAlimentacionTabs();
            initParametrosTabs();
          })
          .catch(function () {
            mainContent.innerHTML =
              '<div class="section-container"><p class="section-text">No se pudo cargar la vista solicitada.</p></div>';
          });
      });
    });
  }

  function initDashboardCharts() {
    if (typeof ApexCharts === "undefined") return;

    var biomasaEl = document.getElementById("chart-biomasa");
    var consumoEl = document.getElementById("chart-consumo");
    var parametrosEl = document.getElementById("chart-parametros");

    if (biomasaEl) {
      var biomasaOptions = {
        chart: {
          type: "bar",
          height: 260,
          toolbar: { show: false }
        },
        series: [
          {
            name: "Biomasa (kg)",
            data: [320, 260, 180]
          }
        ],
        xaxis: {
          categories: ["Trucha arcoíris", "Tilapia", "Carpa común"],
          labels: { style: { colors: "#4b5563" } }
        },
        colors: ["#1c5d99"],
        dataLabels: { enabled: false },
        grid: { borderColor: "#e5e7eb" },
        tooltip: {
          y: {
            formatter: function (val) {
              return val + " kg";
            }
          }
        }
      };
      var biomasaChart = new ApexCharts(biomasaEl, biomasaOptions);
      biomasaChart.render();
    }

    if (consumoEl) {
      var consumoOptions = {
        chart: {
          type: "line",
          height: 260,
          toolbar: { show: false }
        },
        series: [
          {
            name: "Plan de alimento (kg)",
            data: [85, 88, 90, 92, 94, 96, 98]
          },
          {
            name: "Consumo real (kg)",
            data: [82, 86, 89, 93, 91, 95, 97]
          }
        ],
        xaxis: {
          categories: ["Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom"],
          labels: { style: { colors: "#4b5563" } }
        },
        colors: ["#1c5d99", "#639fab"],
        stroke: {
          width: 3,
          curve: "smooth"
        },
        dataLabels: { enabled: false },
        grid: { borderColor: "#e5e7eb" },
        tooltip: {
          y: {
            formatter: function (val) {
              return val + " kg";
            }
          }
        },
        legend: {
          position: "top",
          labels: { colors: "#4b5563" }
        }
      };
      var consumoChart = new ApexCharts(consumoEl, consumoOptions);
      consumoChart.render();
    }

    if (parametrosEl) {
      var parametrosOptions = {
        chart: {
          type: "radialBar",
          height: 260
        },
        series: [92, 88, 81],
        labels: ["Oxígeno", "Temperatura", "pH"],
        colors: ["#1c5d99", "#639fab", "#bbcde5"],
        plotOptions: {
          radialBar: {
            hollow: { size: "40%" },
            dataLabels: {
              name: {
                fontSize: "13px"
              },
              value: {
                fontSize: "14px",
                formatter: function (val) {
                  return val + "% en rango";
                }
              },
              total: {
                show: true,
                label: "Promedio",
                formatter: function () {
                  return "87% en rango";
                }
              }
            }
          }
        },
        legend: {
          show: false
        }
      };
      var parametrosChart = new ApexCharts(parametrosEl, parametrosOptions);
      parametrosChart.render();
    }
  }

  function initUserTabs() {
    var tabsRoot = document.querySelector("[data-user-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-user-tab]");
    var views = document.querySelectorAll("[data-user-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-user-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-user-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-user-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initCrianzaTabs() {
    var tabsRoot = document.querySelector("[data-crianza-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-crianza-tab]");
    var views = document.querySelectorAll("[data-crianza-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-crianza-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-crianza-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-crianza-tab");
        setActive(key);
      });
    });

    setActive("fases");
  }

  function initCosechasTabs() {
    var tabsRoot = document.querySelector("[data-cosechas-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-cosechas-tab]");
    var views = document.querySelectorAll("[data-cosechas-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-cosechas-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-cosechas-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-cosechas-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initLotesTabs() {
    var tabsRoot = document.querySelector("[data-lotes-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-lotes-tab]");
    var views = document.querySelectorAll("[data-lotes-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-lotes-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-lotes-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-lotes-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initEstanquesTabs() {
    var tabsRoot = document.querySelector("[data-estanques-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-estanques-tab]");
    var views = document.querySelectorAll("[data-estanques-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-estanques-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-estanques-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-estanques-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initEspeciesTabs() {
    var tabsRoot = document.querySelector("[data-especies-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-especies-tab]");
    var views = document.querySelectorAll("[data-especies-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-especies-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-especies-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-especies-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initProveedoresTabs() {
    var tabsRoot = document.querySelector("[data-proveedores-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-proveedores-tab]");
    var views = document.querySelectorAll("[data-proveedores-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-proveedores-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-proveedores-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-proveedores-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initAlimentacionTabs() {
    var tabsRoot = document.querySelector("[data-alimentacion-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-alimentacion-tab]");
    var views = document.querySelectorAll("[data-alimentacion-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-alimentacion-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-alimentacion-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-alimentacion-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  function initParametrosTabs() {
    var tabsRoot = document.querySelector("[data-parametros-tabs]");
    if (!tabsRoot) return;

    var buttons = tabsRoot.querySelectorAll("[data-parametros-tab]");
    var views = document.querySelectorAll("[data-parametros-view]");

    function setActive(viewKey) {
      buttons.forEach(function (btn) {
        var key = btn.getAttribute("data-parametros-tab");
        if (key === viewKey) {
          btn.classList.add("tab-button--active");
        } else {
          btn.classList.remove("tab-button--active");
        }
      });

      views.forEach(function (view) {
        var key = view.getAttribute("data-parametros-view");
        view.style.display = key === viewKey ? "" : "none";
      });
    }

    buttons.forEach(function (btn) {
      btn.addEventListener("click", function () {
        var key = btn.getAttribute("data-parametros-tab");
        setActive(key);
      });
    });

    setActive("lista");
  }

  // Inicialización del dashboard (solo se ejecuta si existen los contenedores de gráficos)
  initDashboardCharts();
})();

