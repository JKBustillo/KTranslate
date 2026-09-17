[![Licencia](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

<p align="center">
  <img width="670" src="https://github.com/ramjke/Translumo/assets/29047281/8985049f-ea1c-428e-94be-042ece66cb54">
</p>
  <h1 align="center" style="border: 0">KTranslate</h1>
  <h3 align="center" style="border: 0">Traductor de pantalla en tiempo real, con traducción local</h3>

<p align="center"><a href="../README.md"><strong>English</strong></a> | <strong>Español</strong></p>

## Sobre este fork

KTranslate es un fork de [Translumo](https://github.com/ramjke/Translumo), de ramjke, usado bajo la Licencia Apache 2.0.

Cambios de este fork:

- **Traductor Ollama**: traduce con un modelo que corre en tu propio equipo, así que la aplicación sigue funcionando cuando DeepL y Google responden con un 429 o un captcha.
- **Nombres de personajes**: detecta la etiqueta del personaje que aparece encima del cuadro de diálogo y la deja fuera de la traducción. Además recuerda esos nombres, para que los que son palabras corrientes (Sin, Guilty) no se traduzcan cuando aparecen dentro de una frase.
- **Tema claro y oscuro**, y fuera el banner de Lookupper de la barra lateral de ajustes.
- Ajustes en los atajos de teclado y en la espera antes de traducir texto incompleto del OCR.

La atribución que exige la licencia está en el archivo [NOTICE](../NOTICE).

## Descarga

KTranslate todavía no tiene una versión compilada publicada: clona el repositorio y compílalo desde el código, como se explica en Compilación.

Las versiones compiladas del proyecto original están en la [página de releases de Translumo](https://github.com/ramjke/Translumo/releases).

## Características

- **Alta precisión en el reconocimiento de texto**  
  KTranslate permite combinar varios motores de OCR a la vez. Usa un modelo de aprendizaje automático para puntuar el resultado de cada motor y se queda con el mejor.

  <p align="center">
    <img width="740" src="https://github.com/ramjke/Translumo/assets/29047281/649e5fab-a5de-4c54-a3d8-f7ea95b8f218">
  </p>

- **Pensado para juegos**  
  Diseñado para traducir en tiempo real en juegos de PC, aunque funciona con cualquier aplicación y en cualquier parte de la pantalla.

- **Baja latencia**  
  Varias optimizaciones reducen el impacto en el sistema y el tiempo que pasa entre que el texto aparece y se traduce.

- **Tema claro y oscuro**: se cambia con el interruptor que hay abajo en la barra lateral de ajustes.

- **Motores de OCR integrados**: Windows OCR (recomendado), Tesseract 5.2 (antiguo), EasyOCR (antiguo).

- **Traductores disponibles**: Ollama (local, recomendado), DeepL, Google Translate, Yandex Translate, Naver Papago.

  Ollama traduce en tu propio equipo, sin cuotas ni límites de peticiones. Instala [Ollama](https://ollama.com), descarga el modelo indicado en `OllamaTranslator.cs` (`ollama pull qwen2.5:14b`) y déjalo corriendo.

- **Idiomas reconocidos**: inglés, ruso, japonés, chino (simplificado) y coreano.

- **Idiomas de traducción**: inglés, ruso, japonés, chino (simplificado), coreano, francés, español, alemán, portugués, italiano, vietnamita, tailandés, turco, árabe, griego, portugués de Brasil, polaco, bielorruso, persa, indonesio, búlgaro, checo, danés, estonio, finés, húngaro, lituano, letón, neerlandés, rumano, eslovaco, esloveno, sueco y ucraniano.

## Requisitos del sistema

### Requisitos mínimos para Tesseract y Windows OCR
- Windows 10 versión 2004 (compilación 19041) o posterior, o Windows 11
- Tarjeta gráfica compatible con DirectX 11
- 2 GB de RAM

### Requisitos mínimos para EasyOCR
- Tarjeta NVIDIA compatible con CUDA SDK 11.8 (GTX 750, series 8xxM o 9xx en adelante)
- 8 GB de RAM
- Al menos 5 GB de espacio libre

## Cómo se usa

![Vista previa](https://github.com/ramjke/Translumo/blob/7f4a73ffba0e5a0090ea0bfc3d72acb99832a0f4/docs/preview-EN.gif)

1. Abre los ajustes (**Alt+G**)
2. Elige los idiomas: el de origen para el OCR y el de destino para la traducción
3. Elige los motores de reconocimiento de texto (mira los consejos de uso para saber cuáles conviene activar)
4. Define el área de captura: pulsa **Alt+Q** y selecciona una zona de la pantalla
5. Inicia la traducción (pulsa **~**)

### Motores de OCR recomendados

- Lo recomendable es usar **solo WindowsOCR**.

Tesseract es antiguo, lento y comete muchos errores.  
EasyOCR es aún más lento, consume bastantes recursos (incluida una tarjeta gráfica concreta) y suele dar problemas.

Probablemente lo mejor sería quitar los demás motores y dejar solo WindowsOCR, pero siguen incluidos por motivos históricos.

### Usa el área de captura más pequeña posible
Cuanto menor sea el área, menos letras sueltas del fondo se cuelan en el texto. Además, las áreas grandes tardan más en procesarse.

### Usa una lista de proxies para evitar bloqueos de los traductores
Algunos traductores bloquean a los clientes que envían muchas peticiones. Puedes configurar proxies IPv4 propios o compartidos (con 1 o 2 suele bastar) en **Idiomas → pestaña Proxy**. La aplicación los va alternando para no hacer todas las peticiones desde la misma IP.

Con el traductor Ollama esto no hace falta, porque la traducción no sale de tu equipo.

### Juega en modo ventana o ventana sin bordes, no en pantalla completa
La traducción solo se muestra correctamente encima del juego en esos modos. Si tu juego no los admite, puedes usar herramientas como [Borderless Gaming](https://github.com/Codeusa/Borderless-Gaming).

## Preguntas frecuentes

**P: Sale "Failed to capture screen", o no pasa nada al iniciar la traducción**  
R: Asegúrate de que la ventana de destino está activa. Si hace falta, reinicia KTranslate o vuelve a abrir esa ventana.

**P: Estoy en modo ventana, pero la traducción aparece por debajo del juego**  
R: Con el juego abierto y en primer plano, pulsa el atajo (**Alt+T** por defecto) para ocultar y volver a mostrar la ventana de traducción.

**P: Falló la descarga del paquete de EasyOCR**  
R: Prueba a reinstalarlo conectado a una VPN.

**P: Los atajos de teclado no funcionan**  
R: Puede que otra aplicación los esté capturando antes.

**P: Text detection failed (TesseractOCREngine)**  
R: Comprueba que la ruta de la aplicación solo tenga letras del alfabeto latino.

## Compilación

*Hacen falta Visual Studio 2022 y el SDK de .NET 8.*

1. Clona el repositorio (la rama **master** es siempre la versión más reciente):

    ```bash
    git clone https://github.com/JKBustillo/KTranslate.git
    ```

> Nota: al compilar, **binaries_extract.bat** descarga y extrae automáticamente los modelos y los binarios de Python (unos 400 MB) en la carpeta de salida.

## Créditos

- [Material Design In XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)  
- [Tesseract .NET wrapper](https://github.com/charlesw/tesseract)  
- [OpenCvSharp](https://github.com/shimat/opencvsharp)  
- [Python.NET](https://github.com/pythonnet/pythonnet)  
- [EasyOCR](https://github.com/JaidedAI/EasyOCR)  
- [Silero TTS](https://github.com/snakers4/silero-models)  

## Alternativas

- [Translumo](https://github.com/ramjke/Translumo) — el proyecto en el que se basa este fork.
- [Lookupper](https://lookupper.com) — diccionario y traductor de pantalla para aprender idiomas.
- [ScreTran](https://github.com/PavlikBender/ScreTran) — traductor de pantalla sencillo.
- [ScreenTranslator](https://github.com/OneMoreGres/ScreenTranslator) — herramienta de captura, OCR y traducción.
