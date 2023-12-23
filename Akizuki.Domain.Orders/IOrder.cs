﻿namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子の注文モデル
/// </summary>
public interface IOrder
{
    /// <summary>
    /// 注文ID
    /// </summary>
    public OrderId OrderId { get; }

    /// <summary>
    /// 伝票番号
    /// </summary>
    public SlipNumber SlipNumber { get; }

    /// <summary>
    /// 注文日
    /// </summary>
    public OrderDate OrderDate { get; }

    /// <summary>
    /// 注文内容
    /// </summary>
    public IReadOnlyList<AkizukiComponent> Components { get; }
}