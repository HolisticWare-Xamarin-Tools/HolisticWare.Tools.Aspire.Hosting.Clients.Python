# Aspire Component for Python and more

*   Python

    *   Django

    *   Flask

## TODOs

*   `venv` detection

    https://stackoverflow.com/questions/22003769/get-virtualenvs-bin-folder-path-from-script

*   OTLP

    https://opentelemetry-python.readthedocs.io/en/latest/exporter/otlp/otlp.html

    https://opentelemetry.io/docs/languages/python/

    https://pypi.org/project/opentelemetry-exporter-otlp/

    https://docs.newrelic.com/docs/more-integrations/open-source-telemetry-integrations/opentelemetry/get-started/opentelemetry-tutorial-python/

```
pip install flask
pip install opentelemetry-instrumentation-flask
pip install opentelemetry-exporter-otlp
pip install opentelemetry-distro
```

```
opentelemetry-instrument python3 app.py
```