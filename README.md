# Data File Export

*Data File Export* is a library written in c# for exporting data to various file formats without external dependencies. The library supports writing to DBF, CSV and OfficeXml files. 

The library does not use any external dependencies for DBF files so it can be used without the usual problems with external drivers (eg: 32 bit and 64 bit, versioning, etc.).

The design of the library is oriented toward easily exporting data to files from various data sources like DataSet or list of custom objects. 

# Sample usage

The following exports a DBF file from data in an existing DataSet: 

```c#
    DataFileExport
        .CreateDbf(sourceDataSet)
        .AddTextField("AString", 30)
        .AddNumericField("ANumber", 10, 0)
        .Write(filePath);
```

Writing CSV file instead is as simple as changing CreateDbf with CreateCsv.

