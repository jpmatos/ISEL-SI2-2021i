Contribuinte(**NIF**, nome, morada)

Fatura(**código**, *NIF*, estado, valor_total, IVA, data_de_criação, data_de_emissão)
- RI1: código toma os valores: FTyyyyy-xxxxx, onde yyyy representa o ano e xxxx representa o número da factura emitida num ano
- RI2: data_de_criação e data_de_emissão tem o formato YYYY/MM/DD-HH:MM:SS
- RI3: estado toma os valores:
           -emitida, impossibilitando posteriores alterações à factura;
           -em actualização, quando ainda não está finalizada (e.g. falta adicionar itens);
           -proforma, impossibilitando qualquer alteração à factura, excepto a passagem para os estado
            Emitida ou Anulada;
           -anulada, impossibilitando posteriores alterações à factura;
- CE: {NIF Ref. Contribuinte(NIF)}

Alteração(**id**, *código* data_de_alteração, estado, valor_total, IVA, data_de_criação, data_de_emissão)
- RI1: data_de_criação e data_de_emissão tem o formato YYYY/MM/DD-HH:MM:SS
- CE: {código Ref. Fatura(código)}

Produto(**SKU**, descrição, preço_de_venda, IVA)

Item(**número**, *código*, *SKU*, descrição, unidades, desconto)
- CE: {{código Ref Fatura(código)},
        {SKU Ref Produto(SKU)}}

NotaDeCrédito(**código**, *códigoFatura* estado, valor_total, IVA, data_de_criação, data_de_emissão)
- CE: {códigoFatura Ref Fatura(código)}

ItemAcreditado(**id**, *número*, *código*)
- CE: {{número Ref Item(número)},
        {código Ref NotaDeCrédito(código)}}
