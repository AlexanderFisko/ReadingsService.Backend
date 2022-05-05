using System;

namespace ReadingsService.Backend.Core.Entities;

public class Observation
{
    // Костыль, так как SQLite поддерживает IDENTITY только для несоставных первичных ключей.
    // С другой стороны, можно было ключи всех сущностей сделать типа int, а для Sequence добавить отдельное Guid поле
    public int Id { get; set; }

    public Guid? SequenceId { get; set; }

    public Sequence? Sequence { get; set; }

    public Color Color { get; set; }

    // Прибили гвоздями два разряда для простоты.
    // Можно создать отдельную сущность(ти), записать в массив, сделать конвертер и т.д.
    public byte? FirstDisplay { get; set; }

    public byte? SecondDisplay { get; set; }
}